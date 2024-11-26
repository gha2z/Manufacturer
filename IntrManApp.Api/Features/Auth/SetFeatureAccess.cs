using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Api.Entities;
using Mapster;
using MediatR;
using IntrManApp.Shared.Contract;

namespace IntrManApp.Api.Features.Auth;

public static class SetFeatureAccess
{
    public class Command : IRequest<Result<bool>>
    {
        public Guid UserTypeId { get; set; } = Guid.Empty;
        public Guid FeatureId { get; set; } = Guid.Empty;
        public bool CanView { get; set; } = false;
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.UserTypeId).NotEmpty();
            RuleFor(c => c.FeatureId).NotEmpty();
        }
    }
    internal sealed class Handler(Gha2zErpDbContext dbContext) : IRequestHandler<Command, Result<bool>>
    {
        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var role = await dbContext.UserTypes.FindAsync(request.UserTypeId);
            if (role == null)
            {
                return Result.Failure<bool>(new Error("SetFeatureAccess.Validation", "Role not found"));
            }

            var feature = await dbContext.Features.FindAsync(request.FeatureId);
            if (feature == null)
            {
                return Result.Failure<bool>(new Error("SetFeatureAccess.Validation", "Feature not found"));
            }

            var roleFeature = await dbContext.UserTypeFeatures.FindAsync(request.FeatureId, request.UserTypeId);
            if (roleFeature == null)
            {
                return Result.Failure<bool>(new Error("SetFeatureAccess.Validation", "Role-Feature not found"));
            }

            roleFeature.Accessible = request.CanView;
            await dbContext.SaveChangesAsync();
            return Result.Success(true);
        }
    }
}


public class SetFeatureAccessEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/SetFeatureAccess", async (SetFeatureAccessRequest request, ISender sender) =>
        {
            var Command = request.Adapt<SetFeatureAccess.Command>();

            var result = await sender.Send(Command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Set a feature access for a role",
            Summary = "Set Feature Access",
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
