using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Models.Purchasing;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using Mapster;
using IntrManApp.Shared.Models.Person;
using IntrManApp.Shared.Models.Sales;
using IntrManApp.Shared.Models.Production;
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
              
                MeasurementUnitGroup measurementUnitGroup;
                MeasurementUnit measurementUnitOrder;
                if (request.MeasurementUnitGroupId == Guid.Empty)
                {
                    if (_context.MeasurementUnitGroups.Count() == 0)
                    {
                        measurementUnitGroup = new()
                        {
                            Name = "Weight - Kgs"
                        };
                        _context.Add(measurementUnitGroup);
                        await _context.SaveChangesAsync();
                        measurementUnitOrder = new()
                        {
                            Name = "gram",
                            Quantity = 1,
                            GroupId = measurementUnitGroup.Id
                        };
                        _context.Add(measurementUnitOrder);
                        await _context.SaveChangesAsync();
                        var parentUnit = new MeasurementUnit()
                        {
                            Name = "kilogram",
                            Quantity = 1000,
                            GroupId = measurementUnitGroup.Id,
                            ChildId = measurementUnitOrder.Id
                        };
                        _context.Add(parentUnit);
                        await _context.SaveChangesAsync();
                    } else
                    {
                        measurementUnitGroup = _context.MeasurementUnitGroups.First();
                        measurementUnitOrder = _context.MeasurementUnits
                            .Where(u => u.GroupId.Equals(measurementUnitGroup.Id)).First();
                    }
                }
                else
                {
                    measurementUnitGroup = _context.MeasurementUnitGroups
                        .Where(u => u.Id.Equals(request.MeasurementUnitGroupId)).First();
                    measurementUnitOrder = _context.MeasurementUnits
                        .Where(u => u.GroupId.Equals(request.MeasurementUnitOrderId)).First();
                }
                if (measurementUnitGroup == null || measurementUnitOrder == null)
                {
                    return Result.Failure<Guid>(new Error(
                      "CreateProduct.Validation", "Measurement Unit not found"));
                }

                Location location;
                if(request.LocationId.Equals(Guid.Empty))
                {
                    if(_context.Locations.Count()==0)
                    {
                        location = new Location()
                        {
                            Name = "Warehouse"
                        };
                        _context.Add(location);
                        await _context.SaveChangesAsync();
                    } else
                    {
                        location = _context.Locations.First();
                    }
                } else
                {
                    location = _context.Locations
                        .Where(l => l.Id.Equals(request.LocationId)).First();
                }
                if (location == null)
                {
                    return Result.Failure<Guid>(new Error(
                      "CreateProduct.Validation", "Default location not found"));
                }
                ProductCategory category;
                if (request.CategoryId.Equals(Guid.Empty))
                {
                    if (_context.ProductCategories.Count() == 0)
                    {
                        category = new ProductCategory()
                        {
                            Name = "Unnamed Category"
                        };
                        _context.Add(category);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        category = _context.ProductCategories.First();
                    }
                }
                else
                {
                    category = _context.ProductCategories
                        .Where(l => l.Id.Equals(request.CategoryId)).First();
                }
                if (category == null)
                {
                    return Result.Failure<Guid>(new Error(
                      "CreateProduct.Validation", "Product Category not found"));
                }
                if(_context.Cultures.Count() == 0)
                {
                    var culture = new Culture()
                    {
                        Id = "en-US",
                        Name = "English",
                        ModifiedDate = DateTime.Now
                        
                    }; 
                    _context.Add(culture);
                    await _context.SaveChangesAsync();
                    var chineseCulture = CultureInfo.GetCultureInfo("zh-CN");
                    culture = new Culture()
                    {
                        Id = "zh-CN",
                        Name = chineseCulture.Name,
                        ModifiedDate = DateTime.Now
                    };
                    _context.Add(culture);
                    await _context.SaveChangesAsync();
                }
                var product = new Product
                {
                    CategoryId = category.Id,
                    ProductNumber = request.ProductNumber,
                    IsFinishedGood = request.IsFinishedGood,
                    IsSalable = request.IsSalable,
                    IsUniqueBatchPerOrder = request.IsUniqueBatchPerOrder,
                    SafetyStockLevel = request.SafetyStockLevel,
                    ReorderPoint = request.ReorderPoint,
                    StandardCost = request.StandardCost,
                    ListPrice =request.ListPrice,
                    MeasurementUnitGroupId = measurementUnitGroup.Id,
                    MeasurementUnitOrderId = measurementUnitOrder.Id,
                    OrderQuantity = request.OrderQuantity,
                    DaysToManufacture = request.DaysToManufacture,
                    DaysToExpire = request.DaysToExpire,
                    LocationId = location.Id,
                    ProductRackingPalletCol = request.ProductRackingPalletCol,
                    ProductRackingPalletRow = request.ProductRackingPalletRow,
                    AdditionalInfo = request.AdditionalInfo
                };
               
                _context.Add(product);
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
