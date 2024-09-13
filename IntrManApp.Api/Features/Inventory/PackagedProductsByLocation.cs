using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.Inventory;

public static class PackagedProductsByLocation
{
    public class Query : IRequest<Result<List<EndProductItemDetail>>>
    {
    }

    internal sealed class Handler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<Query, Result<List<EndProductItemDetail>>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<Result<List<EndProductItemDetail>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            List<EndProductItemDetail>? queryResults =
                (List<EndProductItemDetail>?)await connection.QueryAsync<EndProductItemDetail>(
                    "Production.GetPackagedProductsByLocation", commandType: CommandType.StoredProcedure);

            if (queryResults == null)
            {
                return Result.Failure<List<EndProductItemDetail>>(new Error("PackagedProductsByLocation.NotFound", "No products found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class PackagedProductsByLocationEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/PackagedProductsByLocation", async (ISender sender) =>
        {
            var query = new PackagedProductsByLocation.Query();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show list of all end product inventory items by location",
            Summary = "End Product Inventory Items",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag>
            {
                new Microsoft.OpenApi.Models.OpenApiTag
                {
                    Name = "Inventory"
                }
            }
        });
    }
}
