﻿using Carter;
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
    public static class CreateRawMaterialCheckout
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
                RuleFor(product => product.LocationId).NotEmpty();
                RuleFor(product => product.RackingPalletId).NotEmpty();
            }
        }

        internal sealed class Handler : IRequestHandler<Command, Result<Guid>>
        {
            private readonly Gha2zErpDbContext _context;
            private readonly IValidator<Command> _validator;

            public Handler(Gha2zErpDbContext dbContext, IValidator<Command> validator)
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

                using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
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
                                RackingPalletId = item.RackingPalletId,
                                SourceLocationId = inventory.LocationId,
                                SourceRackingPalletId = inventory.RackingPalletId
                            });
                        inventory.LocationId = item.LocationId;
                        inventory.RackingPalletId = item.RackingPalletId;
                        inventory.Flag = 2;

                       
                    }
                    _context.ProductCheckouts.Add(productCheckout);
                    await _context.SaveChangesAsync(cancellationToken);

                    await _context.Database.ExecuteSqlInterpolatedAsync(
                        $"Production.DistributeRawMaterialsCheckout @CheckOutId={productCheckout.Id}", cancellationToken: cancellationToken);

                    await transaction.CommitAsync(cancellationToken);
                    return productCheckout.Id;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
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
            app.MapPost("api/rawMaterialCheckout", async (ProductCheckOutRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateRawMaterialCheckout.Command>();

                var result = await sender.Send(command);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }
                return Results.Ok(result.Value);
            }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
            {
                Description = "Creates a bulk of raw materials check out for production and returns the new created checkout id on successful operation",
                Summary = "Create a bulk of raw materials checkout",
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
