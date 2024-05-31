using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.Production;

public static class GetProductionOrderDetail
{
    public class Query : IRequest<Result<IEnumerable<ProductionOrderDetailResponse>>>
    {
        public Guid Id { get; set; }
    }

    internal sealed class Handler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<Query, Result<IEnumerable<ProductionOrderDetailResponse>>>
    {
        public async Task<Result<IEnumerable<ProductionOrderDetailResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = dbConnectionFactory.CreateOpenConnection();

            IEnumerable<ProductionOrderDetailResponse> queryResults =
                (IEnumerable<ProductionOrderDetailResponse>)await connection.QueryAsync<ProductionOrderDetailResponse>(
                    "Production.GetProductionOrderDetailById", new { request.Id }, commandType: CommandType.StoredProcedure) ?? [];

            return Result.Success(queryResults);
        }
    }
}

public class GetProductionOrderDetailEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/productionOrderDetails/{id}", async (Guid id, ISender sender) =>
        {
            var query = new GetProductionOrderDetail.Query() { Id = id };

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show production order detail",
            Summary = "Production order detail",
            Tags =
            [
                new() {
                    Name = "Production"
                }
            ]
        });
    }
}
