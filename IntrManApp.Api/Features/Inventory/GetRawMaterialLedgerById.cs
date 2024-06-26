using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.Inventory;

public static class GetRawMaterialLedgerById
{
    public class Query : IRequest<Result<IEnumerable<InventoryLedger>>>
    {
        public Guid InventoryId { get; set; }
    }

    internal sealed class Handler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<Query, Result<IEnumerable<InventoryLedger>>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<Result<IEnumerable<InventoryLedger>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            IEnumerable<InventoryLedger>? queryResults =
                (IEnumerable<InventoryLedger>?)await connection.QueryAsync<InventoryLedger>(
                    "Production.RawMaterialLedgerById", new { request.InventoryId }, commandType: CommandType.StoredProcedure);

            if (queryResults == null)
            {
                return Result.Failure<IEnumerable<InventoryLedger>>(new Error("GetRawMaterialLedgerById.NotFound", "No products found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetRawMaterialLedgerByIdEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/rawMaterialInventoryLedger/{id}", async (Guid id, ISender sender) =>
        {
            var query = new GetRawMaterialLedgerById.Query { InventoryId = id };

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show Inventory Ledger based on Inventory Id",
            Summary = "Inventory Ledger by InventoryID",
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
