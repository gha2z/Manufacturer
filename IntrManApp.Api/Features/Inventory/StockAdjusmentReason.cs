using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IntrManApp.Api.Features.Inventory;

public static class GetStockAdjustmentReasons
{
    public class Query : IRequest<Result<List<StockAdjustmentReason>>>
    {

    }

    internal sealed class Handler(Gha2zErpDbContext _context) : IRequestHandler<Query, Result<List<StockAdjustmentReason>>>
    {
      
        public async Task<Result<List<StockAdjustmentReason>>> Handle(Query request, CancellationToken cancellationToken)
        {
          
            var reasons = await _context.DiscrepantReasons.ToListAsync();
            List<StockAdjustmentReason>? results = reasons.Adapt<List<StockAdjustmentReason>>();

            if (reasons == null)
            {
                return Result.Failure<List<StockAdjustmentReason>>(new Error("GetStockAdjustmentReasons.NotFound", "Error getting stock adjustment reasons"));
            }
            else
            {
                return Result.Success(results);
            }
        }
    }
}

public class GetStockAdjustmentReasonsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/StockAdjustmentReasons", async (ISender sender) =>
        {
            var query = new GetStockAdjustmentReasons.Query();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show list of stock adjustment reasons",
            Summary = "Stock Adjustment Reason",
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
