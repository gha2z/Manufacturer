using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.BasicModules;

public static class GetRackingPalletById
{
    public class Query : IRequest<Result<RackingPalletResponse>>
    {
        public Guid Id { get; set; }
    }

    internal sealed class Handler : IRequestHandler<Query, Result<RackingPalletResponse>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public Handler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<Result<RackingPalletResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            RackingPalletResponse? queryResults =
                (RackingPalletResponse?)await connection.QueryFirstOrDefaultAsync<RackingPalletResponse>(
                    "Production.GetRackingPalletById", param: new { request.Id }, commandType: CommandType.StoredProcedure);

            if (queryResults == null)
            {
                return Result.Failure<RackingPalletResponse>(new Error("GetRackingPallets.NotFound", "No RackingPallets found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetRackingPalletByIdEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/RackingPallets/{id}", async (Guid id, ISender sender) =>
        {
            var query = new GetRackingPalletById.Query() { Id = id };

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Get a RackingPallet by BusinessEntityId",
            Summary = "Get a RackingPallet",
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
