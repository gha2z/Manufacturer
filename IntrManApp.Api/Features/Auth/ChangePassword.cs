using Carter;
using Dapper;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IntrManApp.Api.Features.Auth;

public static class ChangePassword
{
    public class Command : IRequest<Result<bool>>
    {
        public Guid UserId { get; set; } = Guid.Empty;
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.OldPassword).NotEmpty();
            RuleFor(c => c.NewPassword).NotEmpty();
        }
    }
    internal sealed class Handler(Gha2zErpDbContext dbContext) : IRequestHandler<Command, Result<bool>>
    {
        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users
                .Where(x => x.Id.Equals(request.UserId) && x.Password.ToLower().Trim() == request.OldPassword.Trim().ToLower())
                .FirstOrDefaultAsync();

           
            if (user != null)
            {
                user.Password = request.NewPassword;
                await dbContext.SaveChangesAsync();

                return Result.Success(true);
            }
            else
            {
                return Result.Failure<bool>(new Error(
                     "ChangePassword.Validation", "User not found"));
            }

        }
    }
}

public class ChangePasswordEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/ChangePassword", async (ChangePasswordRequest request, ISender sender) =>
        {
            var Command = request.Adapt<ChangePassword.Command>();

            var result = await sender.Send(Command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "It is clear enough",
            Summary = "Change user password",
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
