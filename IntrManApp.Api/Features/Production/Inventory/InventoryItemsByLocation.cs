using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.Production;

public static class InventoryItemsByLocation
{
    public class Query : IRequest<Result<List<InventoryItemDetail>>>
    {
        public Guid LocationId { get; set; }
    }

    internal sealed class Handler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<Query, Result<List<InventoryItemDetail>>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<Result<List<InventoryItemDetail>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            List<InventoryItemDetail>? queryResults =
                (List<InventoryItemDetail>?)await connection.QueryAsync<InventoryItemDetail>(
                    "Production.GetInventoryItemsByLocation",  new { request.LocationId }, commandType: CommandType.StoredProcedure);

            if (queryResults == null)
            {
                return Result.Failure<List<InventoryItemDetail>>(new Error("InventoryItemsByLocation.NotFound", "No products found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class InventoryItemsByLocationEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/inventoryItemsByLocation/{locationId}", async (Guid locationId, ISender sender) =>
        {
            var query = new InventoryItemsByLocation.Query() { LocationId = locationId };

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show list of all inventory items by location",
            Summary = "Inventory Items",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag>
            {
                new Microsoft.OpenApi.Models.OpenApiTag
                {
                    Name = "Product"
                }
            }
        });
    }
}
