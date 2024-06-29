using Carter;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IntrManApp.Api.Features.Auth;

public static class GetRoles
{
    public class Query : IRequest<Result<List<ApplicationUserRoleResponse>>>
    {
    }

    internal sealed class Handler(IntrManDbContext dbContext) : IRequestHandler<Query, Result<List<ApplicationUserRoleResponse>>>
    {


        public async Task<Result<List<ApplicationUserRoleResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await dbContext.UserTypes
                .Include(x => x.Users)
                .ToListAsync(cancellationToken);

            List<ApplicationUserRoleResponse> roles = [];
            foreach (var userType in result)
            {
                ApplicationUserRoleResponse role = new()
                {
                    Id = userType.Id,
                    Name = userType.Name ?? string.Empty,
                    ApplicationUsers = userType.Users.Adapt<List<ApplicationUserResponse>>()
                };
                role.FeatureAccess = await dbContext.UserTypeFeatures
                    .Include(x => x.Feature)
                    .Where(x => x.UserTypeId == role.Id && (x.Feature.ParentId==null || x.Feature.ParentId==Guid.Empty))
                    .Select(x => new FeatureAccessResponse
                    {
                        Id = x.FeatureId,
                        Name = x.Feature.Name ?? string.Empty,
                        CanView = x.Accessible ?? false,
                        Icon = x.Feature.Icon ?? string.Empty,
                        Path = x.Feature.Path ?? string.Empty
                    }).ToListAsync();
                foreach (var feature in role.FeatureAccess)
                {
                    feature.ChildrenFeatures = await dbContext.UserTypeFeatures
                       .Include(x => x.Feature)
                       .Where(y => y.UserTypeId == role.Id && y.Feature.ParentId == feature.Id)
                       .Select(x => new FeatureAccessResponse
                       {
                           Id = x.FeatureId,
                           Name = x.Feature.Name ?? string.Empty,
                           CanView = x.Accessible ?? false,
                           Icon = x.Feature.Icon ?? string.Empty,
                           Path = x.Feature.Path ?? string.Empty
                       }).ToListAsync();
                }
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
            var query = new GetRoles.Query();

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
