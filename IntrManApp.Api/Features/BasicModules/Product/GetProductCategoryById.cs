using Carter;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IntrManApp.Api.Features.BasicModules;

public static class GetProductCategoryById
{
    public class Query : IRequest<Result<ProductCategoryResponse>>
    {
        public Guid Id { get; set; }
    }

    internal sealed class Handler(IntrManDbContext context) : IRequestHandler<Query, Result<ProductCategoryResponse>>
    {

        public async Task<Result<ProductCategoryResponse>> Handle(Query request, CancellationToken cancellationToken)
        {


            var ProductCategory = await context.ProductCategories
                .FindAsync([request.Id], cancellationToken: cancellationToken);


            if (ProductCategory == null)
            {
                return Result.Failure<ProductCategoryResponse>(new Error("GetProductCategory.NotFound", "No ProductCategory found"));
            }
            else
            {
                var queryResults = ProductCategory.Adapt<ProductCategoryResponse>();
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetProductCategoryByIdEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/productCategories/{id}", async (Guid id, ISender sender) =>
        {
            var query = new GetProductCategoryById.Query() { Id = id };

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Get a ProductCategory by Id",
            Summary = "Get a ProductCategory",
            Tags = [ new() { Name = "Product" } ]
        });
    }
}
