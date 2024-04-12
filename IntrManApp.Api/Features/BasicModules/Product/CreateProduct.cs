using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using Mapster;
using IntrManApp.Api.Entities;
using System.Globalization;

namespace IntrManApp.Api.Features.BasicModules
{
    public static class CreateProduct
    {
        public class Command : IRequest<Result<Guid>>
        {
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

            public string? ProductRackingPalletCol { get; set; } = string.Empty;

            public short? ProductRackingPalletRow { get; set; } = -1;

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
                        ProductRackingPalletCol = request.ProductRackingPalletCol,
                        ProductRackingPalletRow = request.ProductRackingPalletRow,
                        AdditionalInfo = request.AdditionalInfo
                    };
                    ProductCategory? category;
                    if (request.CategoryId == null || request.CategoryId == Guid.Empty)
                    {
                        if (_context.ProductCategories.Count() == 0)
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
                        if (_context.MeasurementUnitGroups.Count() == 0)
                        {
                            measurementUnitGroup = new()
                            {
                                Name = "Weight - Kgs"
                            };

                            measurementUnitOrder = new()
                            {
                                Name = "gram",
                                Quantity = 1,
                                Group = measurementUnitGroup
                            };

                            var parentUnit = new MeasurementUnit()
                            {
                                Name = "kilogram",
                                Quantity = 1000,
                                Child = measurementUnitOrder,
                                Group  = measurementUnitGroup
                            };
                            
                        }
                        else
                        {
                            measurementUnitGroup = _context.MeasurementUnitGroups.FirstOrDefault();
                            if (measurementUnitGroup != null)
                                measurementUnitOrder = _context.MeasurementUnits
                                    .Where(u => u.GroupId.Equals(measurementUnitGroup.Id)).FirstOrDefault();
                        }
                    }
                    else
                    {
                        measurementUnitGroup = _context.MeasurementUnitGroups
                            .Where(u => u.Id.Equals(request.MeasurementUnitGroupId)).FirstOrDefault();
                        if (measurementUnitGroup != null)
                        {
                            measurementUnitOrder = _context.MeasurementUnits
                            .Where(u => u.GroupId.Equals(request.MeasurementUnitOrderId)).FirstOrDefault();
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
                        if (_context.Locations.Count() == 0)
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


                    if (_context.Cultures.Count() == 0)
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

                        _context.Cultures.AddRange(new[] { culture, chineseCulture });
                    }

                    foreach (var culture in request.ProductNameAndDescriptionCultures)
                    {
                        var newCulture = new ProductNameAndDescriptionCulture()
                        {
                            CultureId = culture.CultureId,
                            Name = culture.Name,
                            Description = culture.Description
                        };
                        product.ProductNameAndDescriptionCultures.Add(newCulture);
                    }

                    _context.Products.Add(product);
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
            app.MapPost("api/createProduct", async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProduct.Command>();
                var result = await sender.Send(command);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }
                return Results.Ok(result.Value);
            });
        }

    }
}
