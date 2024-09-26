using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.BasicModules;

public static class GetUndispatchedOrders
{
    public class Query : IRequest<Result<IEnumerable<DispatchOrderDetail>>>
    {
       
    }

    internal sealed class Handler(IDbConnectionFactory dbConnectionFactory) :
    IRequestHandler<Query, Result<IEnumerable<DispatchOrderDetail>>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<Result<IEnumerable<DispatchOrderDetail>>> Handle(
            Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            IEnumerable<DispatchOrderDetail>? queryResults =
                (IEnumerable<DispatchOrderDetail>?)await connection.QueryAsync<DispatchOrderDetail>(
                    "Sales.GetUndispatchedOrders",  commandType: CommandType.StoredProcedure);

            if (queryResults == null)
            {
                return Result.Failure<IEnumerable<DispatchOrderDetail>>(
                    new Error("GetUndispatchedOrders.NotFound", "No products found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetUndispatchedOrdersEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/undispatchedOrders", async (ISender sender) =>
        {
            var query = new GetUndispatchedOrders.Query();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show list of undispatched orders",
            Summary = "Undispatched Orders",
            Tags =
            [
                new Microsoft.OpenApi.Models.OpenApiTag
                {
                    Name = "Sales"
                }
            ]
        });
    }
}
