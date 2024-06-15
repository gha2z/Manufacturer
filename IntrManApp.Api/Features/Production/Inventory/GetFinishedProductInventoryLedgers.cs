using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.Production;

public static class GetFinishedProductInventoryLedgers
{
    public class Query : IRequest<Result<IEnumerable<InventoryLedger>>>
    {
        public Guid ProductId { get; set; }
        public Guid LocationId { get; set; }
    }

    internal sealed class Handler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<Query, Result<IEnumerable<InventoryLedger>>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<Result<IEnumerable<InventoryLedger>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            IEnumerable<InventoryLedger>? queryResults =
                (IEnumerable<InventoryLedger>?)await connection.QueryAsync<InventoryLedger>(
                    "Production.FinishedProductLedger", new { request.ProductId, request.LocationId }, commandType: CommandType.StoredProcedure);

            if (queryResults == null)
            {
                return Result.Failure<IEnumerable<InventoryLedger>>(new Error("GetFinishedProductInventoryLedgers.NotFound", "No products found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetFinishedProductInventoryLedgersEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/finishedProductInventoryLedger", async (InventoryLedgerRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetFinishedProductInventoryLedgers.Query>();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show Inventory Ledger based on Product Id",
            Summary = "Inventory Ledger",
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
