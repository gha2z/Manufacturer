using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.BasicModules;

public static class GetRawMaterialsForCheckin
{
    public class Query : IRequest<Result<IEnumerable<RawMaterialsForCheckin>>>
    {

    }

    internal sealed class Handler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<Query, Result<IEnumerable<RawMaterialsForCheckin>>>
    {
        public async Task<Result<IEnumerable<RawMaterialsForCheckin>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = dbConnectionFactory.CreateOpenConnection();

            IEnumerable<RawMaterialsForCheckin> queryResults =
                (IEnumerable<RawMaterialsForCheckin>) await connection.QueryAsync<RawMaterialsForCheckin>(
                    "Purchasing.RawMaterialsForCheckin", commandType: CommandType.StoredProcedure) ?? [];

                return Result.Success(queryResults);
        }
    }
}

public class GetRawMaterialsForCheckinEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/rawMaterialsForCheckin", async (ISender sender) =>
        {
            var query = new GetRawMaterialsForCheckin.Query();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show list of raw materials for checkin",
            Summary = "Raw Material List",
            Tags =
            [
                new() {
                    Name = "Product"
                }
            ]
        });
    }
}
