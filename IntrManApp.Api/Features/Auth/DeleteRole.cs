using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IntrManApp.Api.Features.Auth;

public static class DeleteRole
{
    public class Command : IRequest<Result<bool>>
    {
        public Guid Id { get; set; } = Guid.Empty;
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.Id).NotEmpty();
        }
    }
    internal sealed class Handler(Gha2zErpDbContext dbContext) : IRequestHandler<Command, Result<bool>>
    {
        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var userType = await dbContext.UserTypes.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

            if (userType == null)
            {
                return Result.Failure<bool>(new Error("DeleteRole.Validation", "User not found"));
            }
            else
            {
                dbContext.UserTypes.Remove(userType);
                await dbContext.SaveChangesAsync();
                return Result.Success(true);
            }
        }
    }
}

public class DeleteRoleEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/UserRoles/{id}", async (Guid id, ISender sender) =>
        {
            var Command = new DeleteRole.Command { Id = id };

            var result = await sender.Send(Command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Deletes an application user role and returns true on successful operation",
            Summary = "Delete User Role",
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
