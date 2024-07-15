using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Api.Entities;
using Mapster;
using MediatR;
using IntrManApp.Shared.Contract;

namespace IntrManApp.Api.Features.Auth;

public static class CreateRole
{
    public class Command : IRequest<Result<Guid>>
    {
        public string Name { get; set; } = string.Empty;
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.Name).NotEmpty();
        }
    }
    internal sealed class Handler(IntrManDbContext dbContext) : IRequestHandler<Command, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            Guid ret = Guid.Empty;
            try
            {
                dbContext.Database.BeginTransaction();
             
                var role = new UserType
                {
                    Name = request.Name
                };

                await dbContext.UserTypes.AddAsync(role);
                await dbContext.SaveChangesAsync();

                foreach (var feature in dbContext.Features)
                {
                    dbContext.UserTypeFeatures.Add(new UserTypeFeature
                    {
                        FeatureId = feature.Id,
                        UserTypeId = role.Id,
                        Accessible = false
                    });
                }
                await dbContext.SaveChangesAsync();

                dbContext.Database.CommitTransaction();
                ret = role.Id;
            }
            catch 
            {
                dbContext.Database.RollbackTransaction();
            }
            if(ret == Guid.Empty)
            {
                return Result.Failure<Guid>(new Error(
                     "CreateRole.Validation", "Failed adding a new user"));
            } else {
                return Result.Success(ret);
            }
        }
    }
}

public class CreateRoleEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/UserRoles", async (NewOrUpdateRoleRequest request, ISender sender) =>
        {
            var Command = new CreateRole.Command() { Name = request.Name };

            var result = await sender.Send(Command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Creates a new application role and returns Id of the new created one",
            Summary = "New Role",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag>
            {
                new Microsoft.OpenApi.Models.OpenApiTag
                {
                    Name = "Auth"
                }
            }
        });
    }
}
