using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.Production;

public static class GetProductionOrderDetailByDate
{
    public class Query : IRequest<Result<IEnumerable<ProductionOrderDetailResponse>>>
    {
        public DateTime Date { get; set; }
    }

    internal sealed class Handler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<Query, Result<IEnumerable<ProductionOrderDetailResponse>>>
    {
        public async Task<Result<IEnumerable<ProductionOrderDetailResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = dbConnectionFactory.CreateOpenConnection();

            IEnumerable<ProductionOrderDetailResponse> queryResults =
                (IEnumerable<ProductionOrderDetailResponse>)await connection.QueryAsync<ProductionOrderDetailResponse>(
                    "Production.GetProductionOrderDetailByDate", new { request.Date }, commandType: CommandType.StoredProcedure) ?? [];

            return Result.Success(queryResults);
        }
    }
}

public class GetProductionOrderDetailByDateEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/productionOrderDetailsByDate/{date}", async (DateTime date, ISender sender) =>
        {
            var query = new GetProductionOrderDetailByDate.Query() { Date = date };

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show daily production order detail based on supplied date",
            Summary = "Daily Production order detail",
            Tags =
            [
                new() {
                    Name = "Production"
                }
            ]
        });
    }
}
