using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Api.Entities;
using Mapster;
using MediatR;
using IntrManApp.Shared.Contract;

namespace IntrManApp.Api.Features.Auth;

public static class CreateNewUser
{
    public class Command : IRequest<Result<Guid>>
    {
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public Guid TypeId { get; set; } = Guid.Empty;
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Password).NotEmpty();
            RuleFor(c => c.TypeId).NotEmpty();
        }
    }
    internal sealed class Handler(Gha2zErpDbContext dbContext) : IRequestHandler<Command, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            var newUser = new User
            {
                Name = request.Name,
                Password = request.Password,
                TypeId = request.TypeId
            };
            await dbContext.Users.AddAsync(newUser);
            await dbContext.SaveChangesAsync();

            if(newUser.Id != Guid.Empty)
            {
                return Result.Success(newUser.Id);
            }
            else
            {
                return Result.Failure<Guid>(new Error(
                     "CreateNewUser.Validation", "Failed adding a new user"));
            }
        }
    }
}

public class CreateNewUserEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/Users", async (NewUserRequest request, ISender sender) =>
        {
            var Command = request.Adapt<CreateNewUser.Command>();

            var result = await sender.Send(Command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Creates a new application user and returns Id of the new created user",
            Summary = "New User",
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
