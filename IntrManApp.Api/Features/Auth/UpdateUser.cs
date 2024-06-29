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

public static class UpdateUser
{
    public class Command : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public Guid TypeId { get; set; } = Guid.Empty;
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.TypeId).NotEmpty();
            RuleFor(c => c.Id).NotEmpty();
        }
    }
    internal sealed class Handler(IntrManDbContext dbContext) : IRequestHandler<Command, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

            if (user == null)
            {
                return Result.Failure<Guid>(new Error("UpdateUser.Validation", "User not found"));
            } else
            {
                user.Name = request.Name;
                user.TypeId = request.TypeId;
                await dbContext.SaveChangesAsync();
                return Result.Success(user.Id);
            }
        }
    }
}

public class UpdateUserEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/Users", async (UpdateUserRequest request, ISender sender) =>
        {
            var Command = request.Adapt<UpdateUser.Command>();

            var result = await sender.Send(Command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Updates an application user and returns Id of the new created user",
            Summary = "Update User",
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
