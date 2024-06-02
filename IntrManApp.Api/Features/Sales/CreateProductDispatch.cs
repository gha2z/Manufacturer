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
    public static class CreateProductDispatch
    {
        public class Command : IRequest<Result<Guid>>
        {
            public Guid Id { get; set; } = Guid.Empty;
            public DateTime? DispatchDate { get; set; }
            public Guid CustomerId { get; set; }
            public List<DispatchLineRequest> DispatchLines { get; set; } = [];
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleForEach(i => i.DispatchLines).SetValidator(new DispatchLineValidator());
            }
        }

        private class DispatchLineValidator : AbstractValidator<DispatchLineRequest>
        {
            public DispatchLineValidator()
            {
                RuleFor(product => product.InventoryId).NotEmpty();
                RuleFor(product => product.Quantity).GreaterThan(0);
                RuleFor(product => product.MeasurementUnitId).NotEmpty();
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
                        "CreateProductDispatch.Validation", validationResult.ToString()));
                }

                using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
                try
                {

                    var productCheckin = new SalesOrder()
                    {
                        OrderDate = request.DispatchDate,
                        CustomerId = request.CustomerId,
                        ModifiedDate = DateTime.Now,
                        Status = 1,
                        RevisionNumber = 0
                    };

                    foreach (var item in request.DispatchLines)
                    {

                        var inventory = await _context.ProductInventories.FindAsync(item.InventoryId, cancellationToken);
                        if (inventory == null)
                        {
                            return Result.Failure<Guid>(new Error(
                                "CreateProductDispatch.Validation", $"Inventory Item not found"));
                        }

                        productCheckin.SalesOrderLines.Add(
                           new SalesOrderLine()
                           {
                               InventoryId = item.InventoryId,
                               MeasurementUnitId = item.MeasurementUnitId,
                               Quantity = item.Quantity
                           });
                        inventory.Flag = 9;

                    }
                    _context.SalesOrders.Add(productCheckin);
                    await _context.SaveChangesAsync(cancellationToken);

                    await transaction.CommitAsync(cancellationToken);
                    return productCheckin.Id;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return Result.Failure<Guid>(new Error(
                       "CreateProductDispatch.Validation", $"{ex.Message}\n\n{ex}"));
                }
            }
        }
    }

    public class CreateProductDispatchEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/dispatch", async (DispatchRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductDispatch.Command>();

                var result = await sender.Send(command);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }
                return Results.Ok(result.Value);
            }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
            {
                Description = "Creates dispatch entry for a bulk of finished products and returns the new dispatch id on successful operation",
                Summary = "Create dispatch entry",
                Tags = new List<Microsoft.OpenApi.Models.OpenApiTag>
                {
                    new Microsoft.OpenApi.Models.OpenApiTag
                    {
                        Name = "Sales"
                    }
                }
            });
        }

    }
}
