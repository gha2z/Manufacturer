using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.Production;

public static class GetEndProducts
{
    public class Query : IRequest<Result<IEnumerable<EndProduct>>>
    {

    }

    internal sealed class Handler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<Query, Result<IEnumerable<EndProduct>>>
    {
        public async Task<Result<IEnumerable<EndProduct>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = dbConnectionFactory.CreateOpenConnection();

            IEnumerable<EndProduct> queryResults =
                (IEnumerable<EndProduct>)await connection.QueryAsync<EndProduct>(
                    "Purchasing.GetEndProducts", commandType: CommandType.StoredProcedure) ?? [];

            return Result.Success(queryResults);
        }
    }
}

public class GetEndProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/endProducts", async (ISender sender) =>
        {
            var query = new GetEndProducts.Query();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show list of end products",
            Summary = "End Product List",
            Tags =
            [
                new() {
                    Name = "Product"
                }
            ]
        });
    }
}
