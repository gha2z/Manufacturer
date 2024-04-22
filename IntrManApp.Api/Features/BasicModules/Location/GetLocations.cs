using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.BasicModules;

public static class GetLocations
{
    public class Query : IRequest<Result<List<LocationResponse>>>
    {

    }

    internal sealed class Handler : IRequestHandler<Query, Result<List<LocationResponse>>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public Handler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<Result<List<LocationResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            List<LocationResponse>? queryResults =
                (List<LocationResponse>?)await connection.QueryAsync<LocationResponse>(
                    "Production.LocationList", commandType: CommandType.StoredProcedure);

            if (queryResults == null)
            {
                return Result.Failure<List<LocationResponse>>(new Error("GetLocations.NotFound", "No Locations found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetLocationsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/locations", async (ISender sender) =>
        {
            var query = new GetLocations.Query();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show list of Locations",
            Summary = "Location List",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag>
            {
                new Microsoft.OpenApi.Models.OpenApiTag
                {
                    Name = "Location"
                }
            }
        });
    }
}
