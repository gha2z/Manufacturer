﻿using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IntrManApp.Api.Features.BasicModules
{
    public static class DeleteProduct
    {
        public class Command : IRequest<Result<bool>>
        {
            public Guid Id { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Id).NotEmpty();
            }
        }

        internal sealed class Handler(Gha2zErpDbContext dbContext) : IRequestHandler<Command, Result<bool>>
        {
            private readonly Gha2zErpDbContext _context = dbContext;

            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {


                var item = _context.Products
                    .Where(c => c.Id.Equals(request.Id)).FirstOrDefault();
                if (item != null)
                {
                    _context.BillOfMaterials.Where(c => c.ProductId.Equals(request.Id)).ExecuteDelete();
                    _context.Products.Remove(item);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return Result.Failure<bool>(new Error(
                      "DeleteProduct.Validation", "Item not found"));
                }
            }
        }
    }

    public class DeleteProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/products/{id}",
                async (Guid id, ISender sender) =>
                {
                    var command = new DeleteProduct.Command { Id = id };
                    var result = await sender.Send(command);

                    if (result.IsFailure)
                    {
                        return Results.BadRequest(result.Error);
                    }
                    return Results.Ok(result.Value);
                }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
                {
                    Description = "Delete an existing product and returns true on succesful operation",
                    Summary = "Delete an existing product",
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
