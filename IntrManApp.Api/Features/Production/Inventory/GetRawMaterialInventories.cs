using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.Production;

public static class GetRawMaterialInventories
{
    public class Query : IRequest<Result<List<InventoryItem>>>
    {

    }

    internal sealed class Handler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<Query, Result<List<InventoryItem>>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<Result<List<InventoryItem>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            List<InventoryItem>? queryResults =
                (List<InventoryItem>?)await connection.QueryAsync<InventoryItem>(
                    "Production.RawMaterialInventories", commandType: CommandType.StoredProcedure);

            if (queryResults == null)
            {
                return Result.Failure<List<InventoryItem>>(new Error("GetRawMaterialInventories.NotFound", "No products found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetRawMaterialInventoriesEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/rawMaterialInventories", async (ISender sender) =>
        {
            var query = new GetRawMaterialInventories.Query();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show list of raw material inventories based on Location",
            Summary = "Raw Material Inventories",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag>
            {
                new Microsoft.OpenApi.Models.OpenApiTag
                {
                    Name = "Raw Material"
                }
            }
        });
    }
}
