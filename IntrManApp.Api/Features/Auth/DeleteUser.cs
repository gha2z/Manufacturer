using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IntrManApp.Api.Features.Auth;

public static class DeleteUser
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
    internal sealed class Handler(IntrManDbContext dbContext) : IRequestHandler<Command, Result<bool>>
    {
        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

            if (user == null)
            {
                return Result.Failure<bool>(new Error("DeleteUser.Validation", "User not found"));
            }
            else
            {
                dbContext.Users.Remove(user);
                await dbContext.SaveChangesAsync();
                return Result.Success(true);
            }
        }
    }
}

public class DeleteUserEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/Users/{id}", async (Guid id, ISender sender) =>
        {
            var Command = new DeleteUser.Command { Id = id  };

            var result = await sender.Send(Command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Deletes an application user and returns true on successful operation",
            Summary = "Delete User",
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
