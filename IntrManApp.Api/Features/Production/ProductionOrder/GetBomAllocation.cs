using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.Production;

public static class GetBomAllocation
{
    public class Query : IRequest<Result<IEnumerable<BomAllocationResponse>>>
    {
        public Guid InventoryId { get; set; }
    }

    internal sealed class Handler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<Query, Result<IEnumerable<BomAllocationResponse>>>
    {
        public async Task<Result<IEnumerable<BomAllocationResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = dbConnectionFactory.CreateOpenConnection();

            IEnumerable<BomAllocationResponse> queryResults =
                (IEnumerable<BomAllocationResponse>)await connection.QueryAsync<BomAllocationResponse>(
                    "Production.GetBomAllocation", new { request.InventoryId }, commandType: CommandType.StoredProcedure) ?? [];

            return Result.Success(queryResults);
        }
    }
}

public class GetBomAllocationEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/GetBomAllocation/{inventoryId}", async (Guid inventoryId, ISender sender) =>
        {
            var query = new GetBomAllocation.Query() { InventoryId = inventoryId};

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Get BOM allocation for a specified end product batch",
            Summary = "Get BOM Allocation",
            Tags =
            [
                new() {
                    Name = "Production"
                }
            ]
        });
    }
}
