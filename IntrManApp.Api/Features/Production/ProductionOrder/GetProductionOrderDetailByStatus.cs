using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.Production;

public static class GetProductionOrderDetailByStatus
{
    public class Query : IRequest<Result<IEnumerable<ProductionOrderDetailResponse>>>
    {
        public int Flag { get; set; }
    }

    internal sealed class Handler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<Query, Result<IEnumerable<ProductionOrderDetailResponse>>>
    {
        public async Task<Result<IEnumerable<ProductionOrderDetailResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = dbConnectionFactory.CreateOpenConnection();

            IEnumerable<ProductionOrderDetailResponse> queryResults =
                (IEnumerable<ProductionOrderDetailResponse>)await connection.QueryAsync<ProductionOrderDetailResponse>(
                    "Production.GetProductionOrderDetailByStatus", new { request.Flag }, commandType: CommandType.StoredProcedure) ?? [];

            return Result.Success(queryResults);
        }
    }
}

public class GetProductionOrderDetailByStatusEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/productionOrderDetailsByStatus/{flag}", async (int flag, ISender sender) =>
        {
            var query = new GetProductionOrderDetailByStatus.Query() { Flag = flag };

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show production order detail by status",
            Summary = "Production order detail by status",
            Tags =
            [
                new() {
                    Name = "Production"
                }
            ]
        });
    }
}
