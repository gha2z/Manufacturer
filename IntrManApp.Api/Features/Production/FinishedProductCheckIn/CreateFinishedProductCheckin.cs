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
    public static class CreateFinishedProductCheckin
    {
        public class Command : IRequest<Result<Guid>>
        {
            public Guid Id { get; set; } = Guid.Empty;
            public DateTime? CheckInDate { get; set; }
            public byte? CheckInType { get; set; }
            public byte? RevisionNumber { get; set; }
            public DateTime? ModifierDate { get; set; }
            public List<FinishedProductInternalCheckinLineRequest> ProductInternalCheckinLines { get; set; } = [];
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleForEach(i => i.ProductInternalCheckinLines).SetValidator(new EndProductCheckinDetailValidator());
            }
        }

        private class EndProductCheckinDetailValidator : AbstractValidator<FinishedProductInternalCheckinLineRequest>
        {
            public EndProductCheckinDetailValidator()
            {
                RuleFor(product => product.InventoryId).NotEmpty();
                RuleFor(product => product.Quantity).GreaterThan(0);
                RuleFor(product => product.MeasurementUnitId).NotEmpty();
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
                        "CreateFinishedProductCheckin.Validation", validationResult.ToString()));
                }

                using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
                try
                {

                    var productCheckin = new ProductInternalCheckIn() 
                    { 
                         CheckInDate = request.CheckInDate,
                          CheckInType = 1,
                        ModifierDate = DateTime.Now,
                        RevisionNumber = 0
                    };

                    foreach (var item in request.ProductInternalCheckinLines)
                    {
                       
                        var inventory = await _context.ProductInventories.FindAsync(item.InventoryId, cancellationToken);
                        if (inventory == null)
                        {
                            return Result.Failure<Guid>(new Error(
                                "CreateFinishedProductCheckin.Validation", $"Inventory Item not found"));
                        }

                        productCheckin.ProductInternalCheckInLines.Add(
                           new ProductInternalCheckInLine()
                           {
                               InventoryId = item.InventoryId,
                               MeasurementUnitId = item.MeasurementUnitId,
                               Quantity = item.Quantity,
                               LocationId = item.LocationId,
                               RackingPalletId = item.RackingPalletId,
                               SourceLocationId = inventory.LocationId,
                               SourceRackingPalletId = inventory.RackingPalletId,
                               ModifiedDate = DateTime.Now
                           });

                     
                        inventory.LocationId = item.LocationId;
                        inventory.RackingPalletId = item.RackingPalletId;
                        inventory.Flag = 7;
                        inventory.ExpirationDate = item.ExpiryDate;
                        inventory.ProductionDate = request.CheckInDate;

                    }
                    _context.ProductInternalCheckIns.Add(productCheckin);
                    await _context.SaveChangesAsync(cancellationToken);

                    await transaction.CommitAsync(cancellationToken);
                    return productCheckin.Id;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return Result.Failure<Guid>(new Error(
                       "CreateFinishedProductCheckin.Validation", $"{ex.Message}\n\n{ex}"));
                }
            }
        }
    }

    public class CreateFinishedProductCheckinEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/CompleteProduction", async (FinishedProductInternalCheckinRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateFinishedProductCheckin.Command>();

                var result = await sender.Send(command);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }
                return Results.Ok(result.Value);
            }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
            {
                Description = "Creates a bulk of finished products and returns the new created end product internal checkin id on successful operation",
                Summary = "Create a bulk of finished products",
                Tags = new List<Microsoft.OpenApi.Models.OpenApiTag>
                {
                    new Microsoft.OpenApi.Models.OpenApiTag
                    {
                        Name = "Production"
                    }
                }
            });
        }

    }
}
