using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.BasicModules;

public static class GetRackingPallets
{
    public class Query : IRequest<Result<List<RackingPalletResponse>>>
    {

    }

    internal sealed class Handler : IRequestHandler<Query, Result<List<RackingPalletResponse>>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public Handler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<Result<List<RackingPalletResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            List<RackingPalletResponse>? queryResults =
                (List<RackingPalletResponse>?)await connection.QueryAsync<RackingPalletResponse>(
                    "Production.RackingPalletList", commandType: CommandType.StoredProcedure);

            if (queryResults == null)
            {
                return Result.Failure<List<RackingPalletResponse>>(new Error("GetRackingPallets.NotFound", "No RackingPallets found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetRackingPalletsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/rackingPallets", async (ISender sender) =>
        {
            var query = new GetRackingPallets.Query();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show list of RackingPallets",
            Summary = "RackingPallet List",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag>
            {
                new Microsoft.OpenApi.Models.OpenApiTag
                {
                    Name = "RackingPallet"
                }
            }
        });
    }
}
