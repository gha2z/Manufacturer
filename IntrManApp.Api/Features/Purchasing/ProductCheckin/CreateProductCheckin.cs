using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using IntrManApp.Api.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IntrManApp.Api.Features.Purchasing
{
    public static class CreateProductCheckIn
        {
            public class Command : IRequest<Result<Guid>>
            {
                public Guid Id { get; set; } = Guid.Empty;
                public Guid SupplierId { get; set; }
                public DateTime? CheckinDate { get; set; }
                public List<ProductCheckInLineRequest> ProductCheckInDetail { get; set; } = [];
            }

            public class Validator : AbstractValidator<Command>
            {
                public Validator()
                {
                    RuleFor(checkin => checkin.SupplierId).NotEmpty();
                    RuleForEach(checkin => checkin.ProductCheckInDetail).SetValidator(new ProductCheckInDetailValidator());
                }
            }

            private class ProductCheckInDetailValidator : AbstractValidator<ProductCheckInLineRequest>
            {
                public ProductCheckInDetailValidator()
                {
                    RuleFor(product => product.LocationId).NotEmpty();
                    RuleFor(product => product.TotalBatches).GreaterThan(0);
                    RuleFor(product => product.QuantityPerBatch).GreaterThan(0);
                    RuleFor(product => product.ProductionDate).NotEmpty();
                    RuleFor(product => product.ExpirationDate).NotEmpty();
                    RuleFor(product => product.UnitMeasurementId).NotEmpty();
                    RuleFor(product => product.RackingPalletId).NotEmpty();
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
                            "CreateProductcheckin.Validation", validationResult.ToString()));
                    }

                    using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

                    try
                    {
                        var productCheckIn = new ProductCheckIn() 
                        { 
                            CheckInDate = request.CheckinDate,
                            SupplierId = request.SupplierId
                        };
                        for(short i = 0; i < request.ProductCheckInDetail.Count; i++)
                        {
                            var item = request.ProductCheckInDetail[i];
                        var product = await _context.Products.FindAsync([item.ProductId, cancellationToken], cancellationToken: cancellationToken);
                            if (product == null)
                            {
                                return Result.Failure<Guid>(new Error(
                                    "CreateProductCheckIn.Validation", $"Inventory Item not found"));
                            }
                            var productCheckInLine = new ProductCheckInLine
                            {
                                LineId = item.LineId,
                                LineIndex = (short?)(1 + i),
                                ProductId = item.ProductId,
                                TotalBatches = item.TotalBatches,
                                QuantityPerBatch = item.QuantityPerBatch,   
                                MeasurementUnitId = item.UnitMeasurementId,
                                ProductionDate = item.ProductionDate,
                                ExpirationDate = item.ExpirationDate,
                                LocationId = item.LocationId,
                                RackingPalletId = item.RackingPalletId,
                            };
                            productCheckIn.ProductCheckInLines.Add(productCheckInLine);
                        }
                      
                        _context.ProductCheckIns.Add(productCheckIn);
                        await _context.SaveChangesAsync(cancellationToken);

                        var productsCheckInLines = productCheckIn.ProductCheckInLines.ToList();
                        //get the number of checkins for the day
                        var checkInCount = await _context.ProductCheckIns
                            .Where(x => x.CheckInDate == request.CheckinDate)
                            .CountAsync(cancellationToken);

                        //generate unique batch numbers for each product checkin based on the checkin date and total batches
                        foreach (var productCheckInLine in productsCheckInLines)
                        {
                            for (int i = 0; i < productCheckInLine.TotalBatches; i++)
                            {
                                
                                //varify if the batch number already exists
                                var batchExists = true;
                                var batchNumber = string.Empty;
                                while (batchExists)
                                {
                                    checkInCount++;
                                    batchNumber = $"{productCheckIn.CheckInDate:yyyyMMdd}{checkInCount}";
                                    batchExists = await _context.ProductCheckInLineDetails
                                        .AnyAsync(x => x.BatchNumber == batchNumber, cancellationToken);
                                } 
                           
                                var productCheckInLineDetail = new ProductCheckInLineDetail
                                {
                                    LineId = productCheckInLine.LineId,
                                    BatchNumber = batchNumber
                                };
                                productCheckInLine.ProductCheckInLineDetails.Add(productCheckInLineDetail);
                                await _context.SaveChangesAsync(cancellationToken);
                            }
                        }

                        //generate ProductInventories for each product checkin line detail
                        foreach (var productCheckInLine in productsCheckInLines)
                        {
                            foreach (var productCheckInLineDetail in productCheckInLine.ProductCheckInLineDetails)
                            {
                                var productInventory = new ProductInventory
                                {
                                    InventoryId = productCheckInLineDetail.InventoryId,
                                    ProductId = productCheckInLine.ProductId,
                                    Quantity = productCheckInLine.QuantityPerBatch,
                                    MeasurementUnitId = productCheckInLine.MeasurementUnitId,
                                    BatchNumber = productCheckInLineDetail.BatchNumber,
                                    ProductionDate = productCheckInLine.ProductionDate,
                                    ExpirationDate = productCheckInLine.ExpirationDate,
                                    LocationId = productCheckInLine.LocationId,
                                    RackingPalletId = productCheckInLine.RackingPalletId,
                                    ModifiedDate = DateTime.Now,
                                    TransIdReference = productCheckIn.Id,
                                    Flag = 1
                                };
                                _context.ProductInventories.Add(productInventory);
                            }
                        }
                        await _context.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync(cancellationToken);
                        
                        return Result.Success(productCheckIn.Id);
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync(cancellationToken);
                        return Result.Failure<Guid>(new Error(
                           "CreateProductCheckIn.Validation", $"{ex.Message}\n\n{ex}"));
                    }
                }
            }
        }

        public class CreateMaterialcheckinEndPoint : ICarterModule
        {
            public void AddRoutes(IEndpointRouteBuilder app)
            {
                app.MapPost("api/productCheckins", async (ProductCheckinRequest request, ISender sender) =>
                {
                    var command = request.Adapt<CreateProductCheckIn.Command>();

                    var result = await sender.Send(command);

                    if (result.IsFailure)
                    {
                        return Results.BadRequest(result.Error);
                    }
                    return Results.Ok(result.Value);
                }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
                {
                    Description = "Creates a bulk of raw materials checkin in and " +
                    "returns the new created checkin id on successful operation." +
                    "This will generate product inventories with automatic CartonId",
                    Summary = "Create a new raw materials checkin",
                    Tags = [ new() { Name = "Raw Materials Checkin"} ]
                }
                );
            }
        }
    }

