using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IntrManApp.Api.Features.Auth;

public static class Features
{
    public class Query : IRequest<Result<List<FeatureAccess>>>
    {
    }

    internal sealed class Handler(IntrManDbContext dbContext) : IRequestHandler<Query, Result<List<FeatureAccess>>>
    {


        public async Task<Result<List<FeatureAccess>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await dbContext.Features.ToListAsync(cancellationToken);
            List<FeatureAccess> features = result.Adapt<List<FeatureAccess>>();
           
            return Result.Success(features);

        }
    }
}

public class FeaturesEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/features", async (ISender sender) =>
        {
            var query = new Features.Query();

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
