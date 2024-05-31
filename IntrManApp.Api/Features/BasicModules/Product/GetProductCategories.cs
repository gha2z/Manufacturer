using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.BasicModules;

public static class GetProductCategories
{
    public class Query : IRequest<Result<List<ProductCategoryResponse>>>
    {

    }

    internal sealed class Handler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<Query, Result<List<ProductCategoryResponse>>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<Result<List<ProductCategoryResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            List<ProductCategoryResponse>? queryResults =
                (List<ProductCategoryResponse>?)await connection.QueryAsync<ProductCategoryResponse>(
                    "Production.ProductCategoryList", commandType: CommandType.StoredProcedure);

            if (queryResults == null)
            {
                return Result.Failure<List<ProductCategoryResponse>>(new Error("GetProductCategories.NotFound", "No ProductCategories found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetProductCategoriesEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/productCategories", async (ISender sender) =>
        {
            var query = new GetProductCategories.Query();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show list of Product Categories",
            Summary = "Product Cateogry List",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag>
            {
                new Microsoft.OpenApi.Models.OpenApiTag
                {
                    Name = "Product"
                }
            }
        });
    }
}
