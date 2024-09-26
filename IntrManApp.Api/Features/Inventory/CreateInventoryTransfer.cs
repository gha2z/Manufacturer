using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using IntrManApp.Api.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design.Serialization;

namespace IntrManApp.Api.Features.Inventory
{
    public static class CreateInventoryTransfer
    {
        public class Command : IRequest<Result<Guid>>
        {
            public DateTime? CheckoutDate { get; set; }
            public List<ProductCheckOutDetailRequest> ProductInternalCheckOutDetail { get; set; } = [];
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleForEach(checkout => checkout.ProductInternalCheckOutDetail).SetValidator(new ProductCheckOutDetailValidator());
            }
        }

        private class ProductCheckOutDetailValidator : AbstractValidator<ProductCheckOutDetailRequest>
        {
            public ProductCheckOutDetailValidator()
            {
                RuleFor(product => product.InventoryId).NotEmpty();
                RuleFor(product => product.Quantity).GreaterThan(0);
                RuleFor(product => product.UnitMeasurementId).NotEmpty();
                RuleFor(product => product.LocationId).NotEmpty();
                RuleFor(product => product.RackingPalletId).NotEmpty();
            }
        }

        internal sealed class Handler(IntrManDbContext dbContext, IValidator<CreateInventoryTransfer.Command> validator) : IRequestHandler<Command, Result<Guid>>
        {
            private readonly IntrManDbContext _context = dbContext;
            private readonly IValidator<Command> _validator = validator;

            public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = _validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return Result.Failure<Guid>(new Error(
                        "CreateInventoryTransfer.Validation", validationResult.ToString()));
                }

                using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
                try
                {

                    var productInternalCheckOut = new ProductInternalCheckout() { CheckOutDate = request.CheckoutDate };
                    var productInternalCheckIn = new ProductInternalCheckIn() { CheckInDate = request.CheckoutDate };

                    var adjustment = new StockAdjustMent()
                    {
                        AdjustmentDate = DateTime.Now,
                        ModifierDate = DateTime.Now,
                        FromInventoryTransfer = true,
                        RevisionNumber = 0
                    };

                    foreach (var item in request.ProductInternalCheckOutDetail)
                    {
                        var inventory = await _context.ProductInventories.FindAsync(item.InventoryId, cancellationToken);
                        if (inventory == null)
                        {
                            return Result.Failure<Guid>(new Error(
                                "CreateProductInternalCheckOut.Validation", $"Inventory Item not found"));
                        }

                        productInternalCheckOut.ProductInternalCheckOutLines.Add(
                            new ProductInternalCheckOutLine()
                            {
                                InventoryId = item.InventoryId,
                                MeasurementUnitId = item.UnitMeasurementId,
                                Quantity = inventory.Quantity,
                                LocationId = item.LocationId,
                                RackingPalletId = item.RackingPalletId,
                                SourceLocationId = inventory.LocationId,
                                SourceRackingPalletId = inventory.RackingPalletId,
                                ProductionDate = item.ProductionDate,
                                ExpirationDate = item.ExpirationDate
                            });

                        productInternalCheckIn.ProductInternalCheckInLines.Add(
                          new ProductInternalCheckInLine()
                          {
                              InventoryId = item.InventoryId,
                              MeasurementUnitId = item.UnitMeasurementId,
                              Quantity = inventory.Quantity,
                              LocationId = item.LocationId,
                              RackingPalletId = item.RackingPalletId,
                              SourceLocationId = inventory.LocationId,
                              SourceRackingPalletId = inventory.RackingPalletId
                          });

                        inventory.LocationId = item.LocationId;
                        inventory.RackingPalletId = item.RackingPalletId;
                        inventory.ModifiedDate = DateTime.Now;
                        inventory.ProductionDate = item.ProductionDate;
                        inventory.ExpirationDate = item.ExpirationDate;
                        if (inventory.Quantity != item.Quantity)
                        {
                          
                            var reason = await _context.DiscrepantReasons
                                .FirstOrDefaultAsync(x => x.Reason.ToLower().Equals(item.QuantityChangeReason.Trim().ToLower()), cancellationToken);

                            if (reason == null)
                            {
                                reason = new DiscrepantReason()
                                {
                                    Reason = item.QuantityChangeReason.Trim()
                                };
                                _context.DiscrepantReasons.Add(reason);
                                await _context.SaveChangesAsync();
                            }

                            adjustment.StockAdjustmentLines.Add(new StockAdjustmentLine()
                            {
                                InventoryId = item.InventoryId,
                                MeasurementUnitId = item.UnitMeasurementId,
                                Quantity = item.Quantity,
                                InitialQuantity = inventory.Quantity,
                                Adjustment = item.Quantity - inventory.Quantity,
                                ExpirationDate = item.ExpirationDate,
                                ProductionDate = item.ProductionDate, 
                                ReasonId = reason.Id,
                                LocationId = item.LocationId,
                                RackingPalletId = item.RackingPalletId
                            });
                            inventory.Quantity = item.Quantity;
                        }
                    }
                    _context.ProductInternalCheckouts.Add(productInternalCheckOut);
                    _context.ProductInternalCheckIns.Add(productInternalCheckIn);

                    if (adjustment.StockAdjustmentLines.Count > 0)
                    {
                        _context.StockAdjustMents.Add(adjustment);
                    }
                    await _context.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    return productInternalCheckOut.Id;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return Result.Failure<Guid>(new Error(
                       "CreateProductInternalCheckOut.Validation", $"{ex.Message}\n\n{ex}"));
                }
            }
        }
    }

    public class CreateProductInternalCheckOutEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/ProductInternalCheckOut", async (ProductCheckOutRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateInventoryTransfer.Command>();
                command.ProductInternalCheckOutDetail = request.ProductCheckoutDetail.Adapt<List<ProductCheckOutDetailRequest>>();

                var result = await sender.Send(command);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }
                return Results.Ok(result.Value);
            }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
            {
                Description = "Creates a bulk of raw materials/end products check out for production and returns the new created checkout id on successful operation",
                Summary = "Create a bulk of raw materials/end products checkout",
                Tags = new List<Microsoft.OpenApi.Models.OpenApiTag>
                {
                    new Microsoft.OpenApi.Models.OpenApiTag
                    {
                        Name = "Inventory"
                    }
                }
            });
        }

    }
}
