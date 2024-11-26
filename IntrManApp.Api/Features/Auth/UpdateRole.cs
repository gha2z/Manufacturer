using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Api.Entities;
using Mapster;
using MediatR;
using IntrManApp.Shared.Contract;
using Microsoft.EntityFrameworkCore;

namespace IntrManApp.Api.Features.Auth;

public static class UpdateRole
{
    public class Command : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Id).NotEmpty();
        }
    }
    internal sealed class Handler(Gha2zErpDbContext dbContext) : IRequestHandler<Command, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            var userType = await dbContext.UserTypes.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

            if (userType == null)
            {
                return Result.Failure<Guid>(new Error("UpdateRole.Validation", "User not found"));
            }
            else
            {
                userType.Name = request.Name;
                await dbContext.SaveChangesAsync();
                return Result.Success(userType.Id);
            }
        }
    }
}

public class UpdateRoleEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/UserRoles", async (NewOrUpdateRoleRequest request, ISender sender) =>
        {
            var Command = request.Adapt<UpdateRole.Command>();

            var result = await sender.Send(Command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Updates an application role and returns Id of the new created user",
            Summary = "Update Role",
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
