using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using Mapster;
using IntrManApp.Api.Entities;
using System.Globalization;
using Azure.Core;

namespace IntrManApp.Api.Features.BasicModules
{
    public static class CreateProduct
    {
        public class Command : IRequest<Result<Guid>>
        {
            public Guid Id { get; set; } = Guid.Empty;
            public Guid CategoryId { get; set; }

            public string ProductNumber { get; set; } = null!;

            public virtual ICollection<ProductNameRequest> ProductNameAndDescriptionCultures { get; set; } = [];

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
            public Guid OutLocationId { get; set; } = Guid.Empty;
            public Guid OutRackingPalletId { get; set; } = Guid.Empty;

            public string? AdditionalInfo { get; set; }

            public List<ProductVariantRequest> productVariants { get; set; } = [];

        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.ProductNameAndDescriptionCultures.Count).GreaterThan(0);
                
            }
        }

        internal sealed class Handler(IntrManDbContext dbContext, IValidator<CreateProduct.Command> validator) : IRequestHandler<Command, Result<Guid>>
        {
            private readonly IntrManDbContext _context = dbContext;
            private readonly IValidator<Command> _validator = validator;

            public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = _validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return Result.Failure<Guid>(new Error(
                        "CreateProduct.Validation", validationResult.ToString()));
                }

                try
                {
                    var product = new Product
                    {
                        
                        ProductNumber = request.ProductNumber,
                        IsFinishedGood = request.IsFinishedGood,
                        IsSalable = request.IsSalable,
                        IsUniqueBatchPerOrder = request.IsUniqueBatchPerOrder,
                        SafetyStockLevel = request.SafetyStockLevel,
                        ReorderPoint = request.ReorderPoint,
                        StandardCost = request.StandardCost,
                        ListPrice = request.ListPrice,
                        OrderQuantity = request.OrderQuantity,
                        DaysToManufacture = request.DaysToManufacture,
                        DaysToExpire = request.DaysToExpire,
                        AdditionalInfo = request.AdditionalInfo,
                        OutLocationId = request.OutLocationId,
                        OutRackingPalletId = request.OutRackingPalletId
                    };
                    ProductCategory? category;
                    if (request.CategoryId == Guid.Empty)
                    {
                        if (!_context.ProductCategories.Any())
                        {
                            category = new ProductCategory()
                            {
                                Name = "Unnamed Category"
                            };
                            _context.Add(category);
                        }
                        else
                        {
                            category = _context.ProductCategories.FirstOrDefault();
                        }
                    }
                    else
                    {
                        category = _context.ProductCategories
                        .Where(c => c.Id.Equals(request.CategoryId)).FirstOrDefault();
                    }
                    if (category == null)
                    {
                        return Result.Failure<Guid>(new Error(
                            "CreateProduct.Validation", "Please define Product Category"));
                    }
                    product.Category = category;

                    MeasurementUnitGroup? measurementUnitGroup = null;
                    MeasurementUnit? measurementUnitOrder = null;
                    if (request.MeasurementUnitGroupId == Guid.Empty)
                    {
                        if (!_context.MeasurementUnitGroups.Any())
                        {
                            measurementUnitGroup = new()
                            {
                                Name = "Weight - Kgs"
                            };

                            var childUnit = new MeasurementUnit()
                            {
                                Name = "gram",
                                Quantity = 1,
                                Initial = "g",
                                Group = measurementUnitGroup
                            };

                            var parentUnit = new MeasurementUnit()
                            {
                                Name = "kilogram",
                                Quantity = 1000,
                                Child = childUnit,
                                Initial = "kg",
                                Group  = measurementUnitGroup
                            };
                            _context.Add(measurementUnitGroup);
                            _context.Add(childUnit);
                            _context.Add(parentUnit);
                            if(product.IsFinishedGood)
                                measurementUnitOrder = parentUnit;
                            else
                                measurementUnitOrder = childUnit;
                        }
                        else
                        {
                            measurementUnitGroup = _context.MeasurementUnitGroups.FirstOrDefault();
                            if (measurementUnitGroup != null)
                            {
                                if(product.IsFinishedGood)
                                    measurementUnitOrder = _context.MeasurementUnits
                                        .Where(u => u.GroupId.Equals(measurementUnitGroup.Id) && u.Quantity!=1).FirstOrDefault();
                                else
                                    measurementUnitOrder = _context.MeasurementUnits
                                        .Where(u => u.GroupId.Equals(measurementUnitGroup.Id) && u.Quantity==1).FirstOrDefault();
                            }
                        }
                    }
                    else
                    {
                        measurementUnitGroup = _context.MeasurementUnitGroups
                            .Where(u => u.Id.Equals(request.MeasurementUnitGroupId)).FirstOrDefault();
                        if (measurementUnitGroup != null)
                        {
                            if (product.IsFinishedGood)
                                measurementUnitOrder = _context.MeasurementUnits
                                    .Where(u => u.GroupId.Equals(measurementUnitGroup.Id) && u.Quantity != 1).FirstOrDefault();
                            else
                                measurementUnitOrder = _context.MeasurementUnits
                                    .Where(u => u.GroupId.Equals(measurementUnitGroup.Id) && u.Quantity == 1).FirstOrDefault();
                        }
                    }
                    if (measurementUnitGroup == null)
                    {
                        return Result.Failure<Guid>(new Error(
                              "CreateProduct.Validation", "Measurement Unit not found"));
                    }
                    if (measurementUnitOrder == null)
                    {
                        return Result.Failure<Guid>(new Error(
                          "CreateProduct.Validation", "Measurement Unit not found"));
                    }
                    product.MeasurementUnitGroup = measurementUnitGroup;
                    product.MeasurementUnitOrder = measurementUnitOrder;

                    Location? location = null;
                    if (request.LocationId.Equals(Guid.Empty))
                    {
                        if (!_context.Locations.Any())
                        {
                            location = new Location()
                            {
                                Name = "Warehouse"
                            };
                        }
                        else
                        {
                            location = _context.Locations.FirstOrDefault();
                        }
                    }
                    else
                    {
                        location = _context.Locations
                            .Where(l => l.Id.Equals(request.LocationId)).FirstOrDefault();
                    }
                    if (location == null)
                    {
                        return Result.Failure<Guid>(new Error(
                          "CreateProduct.Validation", "Default location not found"));
                    }
                    product.Location = location;

                    RackingPallet? rackingPallet = null;
                    if (request.RackingPalletId.Equals(Guid.Empty))
                    {
                        if (!_context.RackingPallets.Any())
                        {
                            rackingPallet = new RackingPallet()
                            {
                                Col = "A",
                                Row = 1,
                                Description = "A-1"
                            };
                        }
                        else
                        {
                            rackingPallet = _context.RackingPallets.FirstOrDefault();
                        }
                    }
                    else
                    {
                        rackingPallet = _context.RackingPallets
                            .Where(l => l.Id.Equals(request.RackingPalletId)).FirstOrDefault();
                    }
                    if (rackingPallet == null)
                    {
                        return Result.Failure<Guid>(new Error(
                          "CreateProduct.Validation", "Default Racking Pallet not found"));
                    }
                    product.RackingPallet = rackingPallet;


                    if (!_context.Cultures.Any())
                    {
                        var culture = new Culture()
                        {
                            Id = "en-US",
                            Name = "English",
                            ModifiedDate = DateTime.Now

                        };

                        var chCulture = CultureInfo.GetCultureInfo("zh-CN");
                        var chineseCulture = new Culture()
                        {
                            Id = "zh-CN",
                            Name = chCulture.Name,
                            ModifiedDate = DateTime.Now
                        };

                        _context.Cultures.AddRange([culture, chineseCulture]);
                    }

                    foreach (var culture in request.ProductNameAndDescriptionCultures)
                    {
                        var newCulture = new ProductNameAndDescriptionCulture()
                        {
                            CultureId = culture.CultureId,
                            Name = culture.Name,
                            Description = culture.Description ?? string.Empty
                        };
                        product.ProductNameAndDescriptionCultures.Add(newCulture);
                    }

                    product.ProductVariants.Clear();
                    foreach (var variant in request.productVariants)
                    {
                        product.ProductVariants.Add(new ProductVariant()
                        {
                            ProductId = product.Id,
                            MeasurementUnitId = variant.MeasurementUnitId,
                            Weight = variant.Weight
                        });
                    }

                    _context.Add(product);
                    await _context.SaveChangesAsync(cancellationToken);

                    return product.Id;
                } catch (Exception ex)
                {
                    return Result.Failure<Guid>(new Error(
                       "CreateProduct.Validation", $"{ex.Message}\n\n{ex}"));
                }
            }
        }
    }

    public class CreateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/products", async (ProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProduct.Command>();
                command.productVariants = request.ProductVariants.Adapt<List<ProductVariantRequest>>();

                var result = await sender.Send(command);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }
                return Results.Ok(result.Value);
            }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
            {
                Description = "Create new product and returns true on succesful operation",
                Summary = "Create Product",
                Tags = [ new() { Name = "Product" } ]
            });
        }

    }
}
