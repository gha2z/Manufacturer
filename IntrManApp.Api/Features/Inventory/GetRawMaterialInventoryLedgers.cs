using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.Inventory;

public static class GetRawMaterialInventoryLedgers
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
                    "Production.RawMaterialLedger", new { request.ProductId, request.LocationId }, commandType: CommandType.StoredProcedure);

            if (queryResults == null)
            {
                return Result.Failure<IEnumerable<InventoryLedger>>(new Error("GetRawMaterialInventoryLedgers.NotFound", "No products found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetRawMaterialInventoryLedgersEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/rawMaterialInventoryLedger", async (InventoryLedgerRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetRawMaterialInventoryLedgers.Query>();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show Inventory Ledger based on Raw Material Id",
            Summary = "Inventory Ledger",
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
