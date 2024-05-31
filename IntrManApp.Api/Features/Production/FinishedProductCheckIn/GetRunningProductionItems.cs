using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.BasicModules;

public static class GetRunningProductionItems
{
    public class Query : IRequest<Result<IEnumerable<InventoryItem>>>
    {

    }

    internal sealed class Handler(IDbConnectionFactory dbConnectionFactory) :
    IRequestHandler<Query, Result<IEnumerable<InventoryItem>>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<Result<IEnumerable<InventoryItem>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            IEnumerable<InventoryItem>? queryResults =
                (IEnumerable<InventoryItem>?)await connection.QueryAsync<InventoryItem>(
                    "Production.GetRunningProductionItems", commandType: CommandType.StoredProcedure);

            if (queryResults == null)
            {
                return Result.Failure<IEnumerable<InventoryItem>>(new Error("GetRunningProductionItems.NotFound", "No products found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetRunningProductionItemsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/runningProductionItems", async (ISender sender) =>
        {
            var query = new GetRunningProductionItems.Query();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show list of Production Items in Progress",
            Summary = "Running Production Items",
            Tags =
            [
                new Microsoft.OpenApi.Models.OpenApiTag
                {
                    Name = "Production"
                }
            ]
        });
    }
}
