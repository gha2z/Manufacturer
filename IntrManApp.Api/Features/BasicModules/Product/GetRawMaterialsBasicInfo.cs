using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.BasicModules;

public static class GetRawMaterialsBasicInfo
{
    public class Query : IRequest<Result<IEnumerable<RawMaterialBasicInfo>>>
    {

    }

    internal sealed class Handler(IDbConnectionFactory dbConnectionFactory) : 
    IRequestHandler<Query, Result<IEnumerable<RawMaterialBasicInfo>>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<Result<IEnumerable<RawMaterialBasicInfo>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            IEnumerable<RawMaterialBasicInfo>? queryResults =
                (IEnumerable<RawMaterialBasicInfo>?)await connection.QueryAsync<RawMaterialBasicInfo>(
                    "Production.GetRawMaterialsBasicInfo", commandType: CommandType.StoredProcedure);

            if (queryResults == null)
            {
                return Result.Failure<IEnumerable<RawMaterialBasicInfo>>(new Error("GetRawMaterialsBasicInfo.NotFound", "No products found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetRawMaterialsBasicInfoEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/rawMaterialsBasicInfo", async (ISender sender) =>
        {
            var query = new GetRawMaterialsBasicInfo.Query();

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
