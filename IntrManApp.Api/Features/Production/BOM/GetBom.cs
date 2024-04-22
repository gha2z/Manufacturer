using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.BasicModules;

public static class GetBom
{
    public class Query : IRequest<Result<List<BomSpecificationResponse>>>
    {
        public Guid ProductId { get; set; }
    }

    internal sealed class Handler : IRequestHandler<Query, Result<List<BomSpecificationResponse>>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public Handler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<Result<List<BomSpecificationResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            List<BomSpecificationResponse>? queryResults =
                (List<BomSpecificationResponse>?)await connection.QueryAsync<BomSpecificationResponse>(
                    "Production.BomSpecification", new { request.ProductId }, commandType: CommandType.StoredProcedure);

            if (queryResults == null)
            {
                return Result.Failure<List<BomSpecificationResponse>>(new Error("GetBom.NotFound", "No BOM specification found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetBomSpecificationEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/boms/{id}", async (Guid id, ISender sender) =>
        {
            var query = new GetBom.Query() { ProductId = id };

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Get Bill of Material of specified end product",
            Summary = "BOM Specification",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag>
            {
                new Microsoft.OpenApi.Models.OpenApiTag
                {
                    Name = "Bill of Materials"
                }
            }
        });
    }
}
