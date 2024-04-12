using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using IntrManApp.Api.Entities;
using Mapster;
using MediatR;

namespace IntrManApp.Api.Features.Production
{
    public static class CreateProductCheckOut
    {
        public class Command : IRequest<Result<Guid>>
        {
            public DateTime? CheckoutDate { get; set; }
            public List<ProductCheckOutDetailRequest> ProductCheckoutDetail { get; set; } = [];
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleForEach(checkout => checkout.ProductCheckoutDetail).SetValidator(new ProductCheckoutDetailValidator());
            }
        }

        private class ProductCheckoutDetailValidator : AbstractValidator<ProductCheckOutDetailRequest>
        {
            public ProductCheckoutDetailValidator()
            {
                RuleFor(product => product.InventoryId).NotEmpty();
                RuleFor(product => product.Quantity).GreaterThan(0);
                RuleFor(product => product.UnitMeasurementId).NotEmpty();
                RuleFor(product => product.RackingPalletCol).NotEmpty();
                RuleFor(product => product.RackingPalletRow).NotEmpty();
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
                        "CreateMaterialCheckOut.Validation", validationResult.ToString()));
                }

                try
                {
                    var productCheckout = new ProductCheckout() { CheckOutDate = request.CheckoutDate};
                    foreach (var item in request.ProductCheckoutDetail)
                    {
                        var inventory = await _context.ProductInventories.FindAsync(item.InventoryId, cancellationToken);
                        if(inventory == null)
                        {
                            return Result.Failure<Guid>(new Error(
                                "CreateProductCheckOut.Validation", $"Inventory Item not found"));
                        }
                        productCheckout.ProductCheckOutLines.Add(
                            new ProductCheckOutLine()
                            {
                                InventoryId = item.InventoryId,
                                MeasurementUnitId = item.UnitMeasurementId,
                                Quantity = item.Quantity,
                                LocationId = item.LocationId,
                                RackingPalletCol = item.RackingPalletCol,
                                RackingPalletRow = item.RackingPalletRow
                            });
                        inventory.LocationId = item.LocationId;
                        inventory.RackingPalletCol = item.RackingPalletCol;
                        inventory.RackingPalletRow = item.RackingPalletRow;
                    }
                    _context.ProductCheckouts.Add(productCheckout);
                    await _context.SaveChangesAsync(cancellationToken);

                    return productCheckout.Id;
                }
                catch (Exception ex)
                {
                    return Result.Failure<Guid>(new Error(
                       "CreateProductCheckOut.Validation", $"{ex.Message}\n\n{ex}"));
                }
            }
        }
    }

    public class CreateMaterialCheckOutEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/productCheckout", async (ProductCheckOutRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCheckOut.Command>();

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
