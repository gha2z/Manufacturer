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

public static class GetFeatures
{
    public class Query : IRequest<Result<List<FeatureAccessResponse>>>
    {
    }

    internal sealed class Handler(IntrManDbContext dbContext) : IRequestHandler<Query, Result<List<FeatureAccessResponse>>>
    {


        public async Task<Result<List<FeatureAccessResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await dbContext.Features.ToListAsync(cancellationToken);
            List<FeatureAccessResponse> features = result.Adapt<List<FeatureAccessResponse>>();
           
            return Result.Success(features);

        }
    }
}

public class GetFeaturesEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/features", async (ISender sender) =>
        {
            var query = new GetFeatures.Query();

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
