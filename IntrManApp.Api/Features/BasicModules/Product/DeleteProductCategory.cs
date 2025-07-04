﻿using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using MediatR;

namespace IntrManApp.Api.Features.BasicModules
{
    public static class DeleteProductCategory
    {
        public class Command : IRequest<Result<bool>>
        {
            public Guid Id { get; set; }
        }

        internal sealed class Handler(Gha2zErpDbContext dbContext) : IRequestHandler<Command, Result<bool>>
        {
            private readonly Gha2zErpDbContext _context = dbContext;

            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
              
             
                var category = _context.ProductCategories
                    .Where(c => c.Id.Equals(request.Id)).FirstOrDefault();
                if (category != null)
                {
                    _context.ProductCategories.Remove(category);
                    await _context.SaveChangesAsync();
                    return true;
                } else
                {
                    return Result.Failure<bool>(new Error(
                      "DeleteProductCategory.Validation", "Item not found"));
                }
            }
        }
    }

    public class DeleteProductCategoryEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/productCategories/{id}",
                async (Guid id, ISender sender) =>
                {
                    var command = new DeleteProductCategory.Command { Id = id };
                    var result = await sender.Send(command);

                    if (result.IsFailure)
                    {
                        return Results.BadRequest(result.Error);
                    }
                    return Results.Ok(result.Value);
                }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
                {
                    Description = "Deletes an existing product category and returns True on successful operation",
                    Summary = "Delete a product category",
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
}
