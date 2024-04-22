using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using IntrManApp.Api.Entities;
using Mapster;
using MediatR;

namespace IntrManApp.Api.Features.BasicModules
{
    public static class UpdateProduct
    {
        public class Command : IRequest<Result<Guid>>
        {
            public Guid Id { get; set; }
            public Guid? CategoryId { get; set; }

            public string ProductNumber { get; set; } = null!;

            public virtual ICollection<ProductNameAndDescriptionCulture> ProductNameAndDescriptionCultures { get; set; } =
                new List<ProductNameAndDescriptionCulture>();

            public bool IsFinishedGood { get; set; }

            public bool? IsSalable { get; set; }

            public bool? IsUniqueBatchPerOrder { get; set; }

            public decimal? SafetyStockLevel { get; set; }

            public decimal? ReorderPoint { get; set; }

            public decimal? StandardCost { get; set; }

            public decimal? ListPrice { get; set; }

            public Guid MeasurementUnitGroupId { get; set; } = Guid.Empty;

            public Guid MeasurementUnitOrderId { get; set; } = Guid.Empty;

            public decimal? OrderQuantity { get; set; }

            public int? DaysToManufacture { get; set; }

            public int? DaysToExpire { get; set; }

            public Guid LocationId { get; set; } = Guid.Empty;

            public Guid RackingPalletId { get; set; } = Guid.Empty;

            public string? AdditionalInfo { get; set; }

        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.ProductNameAndDescriptionCultures.Count).GreaterThan(0);

            }
        }

        internal sealed class Handler : IRequestHandler<Command, Result<Guid>>
        {
            private readonly IntrManDbContext _context;
            private readonly IValidator<Command> _validator;

            public Handler(IntrManDbContext dbContext, IValidator<Command> validator)
            {
                _context = dbContext;
                _validator = validator;
            }
            public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = _validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return Result.Failure<Guid>(new Error(
                        "UpdateProduct.Validation", validationResult.ToString()));
                }
                var product = _context.Products
                    .Where(p => p.Id.Equals(request.Id)).FirstOrDefault();

                if (product == null)
                {
                    return Result.Failure<Guid>(new Error(
                       "UpdateProduct.Validation", "Product not found!"));
                }

                MeasurementUnitGroup? measurementUnitGroup;
                MeasurementUnit? measurementUnitOrder;

                if (request.MeasurementUnitGroupId == Guid.Empty)
                {
                    measurementUnitGroup = product.MeasurementUnitGroup;
                }
                else
                {
                    measurementUnitGroup = _context.MeasurementUnitGroups
                        .Where(u => u.Id.Equals(request.MeasurementUnitGroupId)).FirstOrDefault();
                }
                if (request.MeasurementUnitOrderId == Guid.Empty)
                {
                    measurementUnitOrder = product.MeasurementUnitOrder;
                }
                else
                { 
                    measurementUnitOrder = _context.MeasurementUnits
                        .Where(u => u.GroupId.Equals(request.MeasurementUnitOrderId)).FirstOrDefault();
                }
                if (measurementUnitGroup == null || measurementUnitOrder == null)
                {
                    return Result.Failure<Guid>(new Error(
                      "UpdateProduct.Validation", "Measurement Unit not found"));
                }

                Location? location;
                if (request.LocationId.Equals(Guid.Empty))
                {
                    location = product.Location;
                }
                else
                {
                    location = _context.Locations
                        .Where(l => l.Id.Equals(request.LocationId)).FirstOrDefault();
                }
                if (location == null)
                {
                    return Result.Failure<Guid>(new Error(
                      "UpdateProduct.Validation", "Default location not found"));
                }
                ProductCategory? category;
                if (request.CategoryId.Equals(Guid.Empty))
                {
                    category = product.Category;
                }
                else
                {
                    category = _context.ProductCategories
                        .Where(l => l.Id.Equals(request.CategoryId)).FirstOrDefault();
                }
                if (category == null)
                {
                    return Result.Failure<Guid>(new Error(
                      "UpdateProduct.Validation", "Product Category not found"));
                }


                product.CategoryId = category.Id;
                product.ProductNumber = request.ProductNumber;
                product.IsFinishedGood = request.IsFinishedGood;
                product.IsSalable = request.IsSalable;
                product.IsUniqueBatchPerOrder = request.IsUniqueBatchPerOrder;
                product.SafetyStockLevel = request.SafetyStockLevel;
                product.ReorderPoint = request.ReorderPoint;
                product.StandardCost = request.StandardCost;
                product.ListPrice = request.ListPrice;
                product.MeasurementUnitGroupId = measurementUnitGroup.Id;
                product.MeasurementUnitOrderId = measurementUnitOrder.Id;
                product.OrderQuantity = request.OrderQuantity;
                product.DaysToManufacture = request.DaysToManufacture;
                product.DaysToExpire = request.DaysToExpire;
                product.LocationId = location.Id;
                product.RackingPalletId = request.RackingPalletId;
                product.AdditionalInfo = request.AdditionalInfo;

                product.ProductNameAndDescriptionCultures.Clear();
                await _context.SaveChangesAsync();
                foreach (var culture in request.ProductNameAndDescriptionCultures)
                {
                    var newCulture = new ProductNameAndDescriptionCulture()
                    {
                        ProductId = product.Id,
                        CultureId = culture.CultureId,
                        Name = culture.Name,
                        Description = culture.Description
                    };
                    _context.Add(newCulture);
                }
                await _context.SaveChangesAsync();

                return product.Id;
            }
        }
    }

    public class UpdateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("api/products", async (UpdateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProduct.Command>();
                var result = await sender.Send(command);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }
                return Results.Ok(result.Value);
            }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
            {
                Description = "Updates the existing product and returns the updated product id on succesful operation",
                Summary = "Update an existing product",
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
