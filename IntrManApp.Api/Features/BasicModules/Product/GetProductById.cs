using Carter;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IntrManApp.Api.Features.BasicModules;

public static class GetProductById
{
    public class Query : IRequest<Result<ProductRequest>>
    {
        public Guid Id { get; set; }
    }

    internal sealed class Handler(IntrManDbContext context) : IRequestHandler<Query, Result<ProductRequest>>
    {
       
        public async Task<Result<ProductRequest>> Handle(Query request, CancellationToken cancellationToken)
        {


            var product = await context.Products
                .Include(x => x.ProductNameAndDescriptionCultures)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);


            if (product == null)
            {
                return Result.Failure<ProductRequest>(new Error("GetProducts.NotFound", "No Products found"));
            }
            else
            {
                var queryResults = product.Adapt<ProductRequest>();
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetProductByIdEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/products/{id}", async (Guid id, ISender sender) =>
        {
            var query = new GetProductById.Query() { Id = id };

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Get a Product by Id",
            Summary = "Get a Product",
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
