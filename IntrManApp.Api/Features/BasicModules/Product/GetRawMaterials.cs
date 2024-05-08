using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.BasicModules;

public static class GetRawMaterialsIdNames
{
    public class Query : IRequest<Result<IEnumerable<RawMaterialIdNames>>>
    {

    }

    internal sealed class Handler(IDbConnectionFactory dbConnectionFactory) : 
    IRequestHandler<Query, Result<IEnumerable<RawMaterialIdNames>>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<Result<IEnumerable<RawMaterialIdNames>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            IEnumerable<RawMaterialIdNames>? queryResults =
                (IEnumerable<RawMaterialIdNames>?)await connection.QueryAsync<RawMaterialIdNames>(
                    "Production.GetRawMaterialsIdNames", commandType: CommandType.StoredProcedure);

            if (queryResults == null)
            {
                return Result.Failure<IEnumerable<RawMaterialIdNames>>(new Error("GetRawMaterialsIdNames.NotFound", "No products found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetRawMaterialsIdNamesEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/rawMaterials", async (ISender sender) =>
        {
            var query = new GetRawMaterialsIdNames.Query();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show list of Id and Names of Raw Materials",
            Summary = "Raw Materials Id & Names List",
            Tags =
            [
                new Microsoft.OpenApi.Models.OpenApiTag
                {
                    Name = "Product"
                }
            ]
        });
    }
}
