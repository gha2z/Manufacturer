using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Api.Entities;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using Mapster;

namespace IntrManApp.Api.Features.BasicModules
{
    public static class UpdateProductCategory
    {
        public class Command : IRequest<Result<Guid>>
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        public class Validator: AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c=>c.Name).NotEmpty();
            }
        }

        internal sealed class Handler(IntrManDbContext dbContext, IValidator<UpdateProductCategory.Command> validator) : IRequestHandler<Command, Result<Guid>>
        {
            private readonly IntrManDbContext _context = dbContext;
            private readonly IValidator<Command> _validator = validator;

            public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = _validator.Validate(request);
                
                if (!validationResult.IsValid)
                {
                    return Result.Failure<Guid>(new Error(
                        "UpdateProductCategory.Validation", validationResult.ToString()));
                }
                var category = _context.ProductCategories
                    .Where(c => c.Id.Equals(request.Id)).FirstOrDefault();
               if (category != null)
                {
                    category.Name = request.Name;
                    await _context.SaveChangesAsync();
                    return category.Id;
                }
                else
                {
                    return Result.Failure<Guid>(new Error(
                      "UpdateProductCategory.Validation", "Item not found"));
                }
            }
        }
    }

    public class UpdateProductCategoryEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("api/productCategories", 
                async (ProductCategoryRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCategory.Command>();
                var result = await sender.Send(command);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }
                return Results.Ok(result.Value);
            }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
            {
                Description = "Updates an existing product category and returns the updated product category id on successful operation",
                Summary = "Update product category",
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
