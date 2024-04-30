using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.BasicModules;

public static class GetLocationById
{
    public class Query : IRequest<Result<LocationResponse>>
    {
        public Guid Id { get; set; }
    }

    internal sealed class Handler : IRequestHandler<Query, Result<LocationResponse>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public Handler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<Result<LocationResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            LocationResponse? queryResults =
                (LocationResponse?)await connection.QueryFirstOrDefaultAsync<LocationResponse>(
                    "Production.GetLocationById", param: new { request.Id }, commandType: CommandType.StoredProcedure);

            if (queryResults == null)
            {
                return Result.Failure<LocationResponse>(new Error("GetLocations.NotFound", "No Locations found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetLocationByIdEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/locations/{id}", async (Guid id, ISender sender) =>
        {
            var query = new GetLocationById.Query() { Id = id };

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Get a Location by Id",
            Summary = "Get a Location",
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
