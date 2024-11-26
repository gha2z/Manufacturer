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
    public static class CreateEndProductTransfer
    {
        public class Command : IRequest<Result<Guid>>
        {
            public DateTime? CheckoutDate { get; set; }
            public List<EndProductCheckOutDetailRequest> ProductInternalCheckOutDetail { get; set; } = [];
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleForEach(checkout => checkout.ProductInternalCheckOutDetail).SetValidator(new ProductCheckOutDetailValidator());
            }
        }

        private class ProductCheckOutDetailValidator : AbstractValidator<EndProductCheckOutDetailRequest>
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

        internal sealed class Handler(Gha2zErpDbContext dbContext, IValidator<CreateEndProductTransfer.Command> validator) : IRequestHandler<Command, Result<Guid>>
        {
            private readonly Gha2zErpDbContext _context = dbContext;
            private readonly IValidator<Command> _validator = validator;

            public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = _validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return Result.Failure<Guid>(new Error(
                        "CreateEndProductTransfer.Validation", validationResult.ToString()));
                }

                using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
                try
                {

                    var productInternalCheckOut = new ProductInternalCheckout() { CheckOutDate = request.CheckoutDate };
                    var productInternalCheckIn = new ProductInternalCheckIn() { CheckInDate = request.CheckoutDate };

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
                                Weight = inventory.Quantity,
                                Quantity = item.Quantity,
                                LocationId = item.LocationId,
                                RackingPalletId = item.RackingPalletId,
                                SourceLocationId = inventory.LocationId,
                                SourceRackingPalletId = inventory.RackingPalletId,
                                ProductionDate = item.ProductionDate,
                                ExpirationDate = item.ExpirationDate
                            });

                        var movedInventory = await _context.ProductInventories.Where(
                            x => x.TransIdReference == inventory.TransIdReference &&
                                x.LocationId == item.LocationId && x.Quantity == item.ItemDetail.Weight && 
                                x.RackingPalletId == item.RackingPalletId).FirstOrDefaultAsync();

                        bool createNewInventory = movedInventory == null;

                        if (movedInventory == null)
                        {
                            movedInventory = inventory.Adapt<ProductInventory>();
                            movedInventory.InventoryId = Guid.NewGuid();
                            movedInventory.TotalBatches = item.Quantity;
                        }
                        else
                        {
                            movedInventory.TotalBatches += item.Quantity;
                        }
                        movedInventory.LocationId = item.LocationId;
                        movedInventory.RackingPalletId = item.RackingPalletId;
                        movedInventory.ModifiedDate = DateTime.Now;

                        if(createNewInventory) _context.ProductInventories.Add(movedInventory);

                        productInternalCheckIn.ProductInternalCheckInLines.Add(
                          new ProductInternalCheckInLine()
                          {
                              InventoryId = movedInventory.InventoryId,
                              MeasurementUnitId = item.UnitMeasurementId,
                              Weight = inventory.Quantity,
                              Quantity = item.Quantity,
                              LocationId = item.LocationId,
                              RackingPalletId = item.RackingPalletId,
                              SourceLocationId = inventory.LocationId,
                              SourceRackingPalletId = inventory.RackingPalletId
                          });

                    
                        inventory.ModifiedDate = DateTime.Now;
                        inventory.ProductionDate = item.ProductionDate;
                        inventory.ExpirationDate = item.ExpirationDate;
                        inventory.TotalBatches -= item.Quantity;
                    }

                    _context.ProductInternalCheckouts.Add(productInternalCheckOut);
                    _context.ProductInternalCheckIns.Add(productInternalCheckIn);

                 
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

    public class CreateEndProductInternalCheckOutEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/EndProductInternalCheckOut", async (EndProductCheckOutRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateEndProductTransfer.Command>();
                command.ProductInternalCheckOutDetail = request.ProductCheckoutDetail.Adapt<List<EndProductCheckOutDetailRequest>>();

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
