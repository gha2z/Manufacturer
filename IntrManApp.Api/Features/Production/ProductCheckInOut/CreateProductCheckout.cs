using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using IntrManApp.Api.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IntrManApp.Api.Features.Production
{
    public static class CreateProductInternalCheckOut
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
                        "CreateInventoryTransfer.Validation", validationResult.ToString()));
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
                                Quantity = item.Quantity,
                                LocationId = item.LocationId,
                                RackingPalletId = item.RackingPalletId,
                                SourceLocationId = inventory.LocationId,
                                SourceRackingPalletId = inventory.RackingPalletId
                            });
                        
                        productInternalCheckIn.ProductInternalCheckInLines.Add(
                          new ProductInternalCheckInLine()
                          {
                              InventoryId = item.InventoryId,
                              MeasurementUnitId = item.UnitMeasurementId,
                              Quantity = item.Quantity,
                              LocationId = item.LocationId,
                              RackingPalletId = item.RackingPalletId,
                              SourceLocationId = inventory.LocationId,
                              SourceRackingPalletId = inventory.RackingPalletId
                          });
                        
                        inventory.LocationId = item.LocationId;
                        inventory.RackingPalletId = item.RackingPalletId;
                        inventory.Flag = 13;
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

    public class CreateProductInternalCheckOutEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/ProductInternalCheckOut", async (ProductCheckOutRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductInternalCheckOut.Command>();
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
                        Name = "Raw Materials Checkout"
                    }
                }
            });
        }

    }
}
