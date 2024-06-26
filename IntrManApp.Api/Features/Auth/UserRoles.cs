using Carter;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IntrManApp.Api.Features.Auth;

public static class UserRoles
{
    public class Query : IRequest<Result<List<UserRole>>>
    {
    }

    internal sealed class Handler(IntrManDbContext dbContext) : IRequestHandler<Query, Result<List<UserRole>>>
    {


        public async Task<Result<List<UserRole>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await dbContext.UserTypes
                .Include(x => x.Users)
                .ToListAsync(cancellationToken);

            List<UserRole> roles = [];
            foreach (var userType in result)
            {
                UserRole role = new()
                {
                    Id = userType.Id,
                    Name = userType.Name?? string.Empty,
                    ApplicationUsers = userType.Users.Adapt<List<ApplicationUser>>()
                };
                role.FeatureAccess = await dbContext.UserTypeFeatures
                    .Include(x => x.Feature)
                    .Where(x => x.UserTypeId == role.Id)
                    .Select(x => new FeatureAccess
                    {
                        Id = x.FeatureId,
                        Name = x.Feature.Name ?? string.Empty,
                        CanView = x.Accessible ?? false,
                        Icon = x.Feature.Icon ?? string.Empty,
                        Path = x.Feature.Path ?? string.Empty,
                        ParentId = x.Feature.ParentId ?? Guid.Empty
                    }).ToListAsync();
                roles.Add(role);
            }
            

            return Result.Success(roles);

        }
    }
}

public class UserRolesEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/userRoles", async (ISender sender) =>
        {
            var query = new UserRoles.Query();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Returns list of app features",
            Summary = "App Features",
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
