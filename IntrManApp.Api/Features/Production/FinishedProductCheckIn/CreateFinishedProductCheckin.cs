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
                //RuleFor(product => product.InventoryId).NotEmpty();
                //RuleFor(product => product.Quantity).GreaterThan(0);
                //RuleFor(product => product.MeasurementUnitId).NotEmpty();
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
                //var validationResult = _validator.Validate(request);
                //if (!validationResult.IsValid)
                //{
                //    return Result.Failure<Guid>(new Error(
                //        "CreateFinishedProductCheckin.Validation", validationResult.ToString()));
                //}

                using var transaction1 = await _context.Database.BeginTransactionAsync(cancellationToken);
                //try
                //{

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
                        var product = await _context.Products.FindAsync([item.ProductId], cancellationToken: cancellationToken);

                        if(product==null)
                        {
                            return Result.Failure<Guid>(new Error(
                                "CreateFinishedProductCheckin.Validation", $"Product with id {item.ProductId} not found"));
                        }

                        if (inventory == null)
                        {
                            inventory = new ProductInventory()
                            {
                                InventoryId = item.InventoryId,
                                ProductId = item.ProductId,
                                BatchNumber = item.BatchNumber,
                                LocationId = product.LocationId,
                                RackingPalletId = product.RackingPalletId,
                                MeasurementUnitId = item.MeasurementUnitId,
                                TotalBatches = 1,
                                Quantity = item.Weight,
                                ModifiedDate = DateTime.Now
                            };
                            _context.ProductInventories.Add(inventory);
                        }

                        var line = new ProductInternalCheckInLine()
                        {
                            InventoryId = item.InventoryId,
                            MeasurementUnitId = item.MeasurementUnitId,
                            Quantity = item.Quantity,
                            Weight = item.Weight,
                            ModifiedDate = DateTime.Now
                        };

                        productCheckin.ProductInternalCheckInLines.Add(line);
                     
                        inventory.Flag = 16;
                        inventory.ExpirationDate = item.ExpiryDate;
                        inventory.ProductionDate = request.CheckInDate;
                        //inventory.TotalBatches = 0;
                       
                        var order = await _context.ProductionOrderLineDetails.FindAsync([inventory.InventoryId], cancellationToken: cancellationToken);
                        if(product!=null && order!=null)
                        {
                            TimeSpan? daysToManufacture = (request.CheckInDate - order.StartDate);
                            product.DaysToManufacture = daysToManufacture.HasValue? daysToManufacture.Value.Days : 0;
                        }
                       
                        foreach (var pack in item.FinishedPackagedProducts)
                        {
                            var packaging = new ProductInternalCheckInLinePackaging()
                            {
                                MeasurementUnitId = pack.ProductVariant.MeasurementUnitId,
                                Weight = pack.ProductVariant.Weight,
                                Quantity = pack.Quantity,
                                LocationId = pack.LocationId,
                                RackingPalletId = pack.RackingPalletId,
                                SourceLocationId = inventory.LocationId,
                                SourceRackingPalletId = inventory.RackingPalletId
                            };
                        line.ProductInternalCheckInLinePackagings.Add(packaging);
                           
                        }

                    }
                    _context.ProductInternalCheckIns.Add(productCheckin);
                    await _context.SaveChangesAsync(cancellationToken);

                await transaction1.CommitAsync(cancellationToken);


                foreach (var item in productCheckin.ProductInternalCheckInLines)
                {
                    foreach (var packaging in item.ProductInternalCheckInLinePackagings)
                    {

                        var productionOfDayCount = await _context.ProductInternalCheckIns
                            .Where(x => EF.Functions.DateDiffDay(x.CheckInDate, request.CheckInDate) == 0)
                            .CountAsync(cancellationToken);

                        var batchExists = true;
                        var batchNumber = string.Empty;
                        while (batchExists)
                        {
                            batchNumber = $"{request.CheckInDate:ddMMyyyy}{productionOfDayCount + 1}";
                            batchExists = await _context.ProductInventories
                                .AnyAsync(x => x.BatchNumber == batchNumber, cancellationToken);
                            productionOfDayCount++;
                        }

                        var inventory = await _context.ProductInventories.FindAsync(item.InventoryId, cancellationToken);

                        var packagingInventory = new ProductInventory()
                        {
                            InventoryId = packaging.InventoryId,
                            ProductId = inventory.ProductId,
                            BatchNumber = batchNumber,
                            LocationId = packaging.LocationId,
                            RackingPalletId = packaging.RackingPalletId,
                            MeasurementUnitId = packaging.MeasurementUnitId,
                            Quantity = (decimal)packaging.Weight,
                            TotalBatches = packaging.Quantity,
                            ProductionDate = inventory.ProductionDate,
                            ExpirationDate = inventory.ExpirationDate,
                            Flag = 8,
                            ModifiedDate = DateTime.Now
                        };

                        _context.ProductInventories.Add(packagingInventory);
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                }
                //await _context.SaveChangesAsync(cancellationToken);
                return productCheckin.Id;
                //}
                //catch (Exception ex)
                //{
                //    await transaction.RollbackAsync(cancellationToken);
                //    return Result.Failure<Guid>(new Error(
                //       "CreateFinishedProductCheckin.Validation", $"{ex.Message}\n\n{ex}"));
                //}
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
                command.ProductInternalCheckinLines = request.ProductInternalCheckinLines.Adapt<List<FinishedProductInternalCheckinLineRequest>>();
            
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
