using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using IntrManApp.Api.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IntrManApp.Api.Features.Inventory
{
    public static class CreateEndProductStockAdjustment
    {
        public class Command : IRequest<Result<Guid>>
        {
            public List<EndProductStockAdjustmentLineRequest> Items { get; set; } = [];
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleForEach(checkout => checkout.Items).SetValidator(new EndProductStockAdjustmentItemsValidator());
            }
        }

        private class EndProductStockAdjustmentItemsValidator : AbstractValidator<EndProductStockAdjustmentLineRequest>
        {
            public EndProductStockAdjustmentItemsValidator()
            {
                RuleFor(i => i.InventoryId).NotEmpty();
                RuleFor(i => i.Quantity).NotNull();
                RuleFor(i => i.InitialQuantity).NotNull();
                RuleFor(i => i.UnitMeasurementId).NotEmpty();
                RuleFor(i => i.Reason).NotEmpty();
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
                        "CreateEndProductStockAdjustment.Validation", validationResult.ToString()));
                }

                using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
                try
                {

                    var adjustment = new StockAdjustMent()
                    {
                        AdjustmentDate = DateTime.Now,
                        ModifierDate = DateTime.Now,
                        RevisionNumber = 0
                    };

                    foreach (var item in request.Items)
                    {
                        var inventory = await _context.ProductInventories.FindAsync(item.InventoryId, cancellationToken);
                        if (inventory == null)
                        {
                            return Result.Failure<Guid>(new Error(
                                "CreateProductInternalCheckOut.Validation", $"Inventory Item not found"));
                        }

                        var reason = await _context.DiscrepantReasons
                            .FirstOrDefaultAsync(x => x.Reason.ToLower().Trim().Equals(item.Reason.ToLower().Trim()), cancellationToken);
                        if (reason == null)
                        {
                            reason = new DiscrepantReason()
                            {
                                Reason = item.Reason
                            };
                            _context.DiscrepantReasons.Add(reason);
                            await _context.SaveChangesAsync(cancellationToken);
                        }

                        adjustment.StockAdjustmentLines.Add(
                            new StockAdjustmentLine()
                            {
                                InventoryId = item.InventoryId,
                                MeasurementUnitId = item.UnitMeasurementId,
                                InitialQuantity = inventory.TotalBatches,
                                Quantity = item.Quantity,
                                Adjustment = item.Quantity - inventory.TotalBatches,
                                Weight = inventory.Quantity,
                                ExpirationDate = item.ExpirationDate,
                                ProductionDate = item.ProductionDate,
                                ReasonId = reason.Id,
                                LocationId = inventory.LocationId,
                                RackingPalletId = inventory.RackingPalletId
                            }
                         );
                       
                        inventory.TotalBatches = item.Quantity;
                        inventory.ModifiedDate = DateTime.Now;
                        inventory.ProductionDate = item.ProductionDate;
                        inventory.ExpirationDate = item.ExpirationDate;
                    }
                    _context.StockAdjustMents.Add(adjustment);

                    await _context.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    return adjustment.Id;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return Result.Failure<Guid>(new Error(
                       "CreateEndProductStockAdjustment.Validation", $"{ex.Message}\n\n{ex}"));
                }
            }
        }
    }

    public class CreateEndProductStockAdjustmentEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/EndProductStockAdjustment", async (EndProductStockAdjustmentRequest request, ISender sender) =>
            {
                var command = new CreateEndProductStockAdjustment.Command() { Items = request.Items };


                var result = await sender.Send(command);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }
                return Results.Ok(result.Value);
            }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
            {
                Description = "Adjust weight, production date and expiry date of bulk of inventory items and returns the new created stock adjustment id on successful operation",
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
