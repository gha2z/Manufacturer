using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using IntrManApp.Api.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Carter;

namespace IntrManApp.Api.Features;

public static class CreateProductionOrder
{
    public class Command : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; } = Guid.Empty;
        public DateTime? OrderDate { get; set; } = DateTime.Now;
        public DateTime? ScheduleDate { get; set; } = DateTime.Now;
        public List<ProductionOrderLineRequest> ProductionOrderLines { get; set; } = [];
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleForEach(order => order.ProductionOrderLines).SetValidator(new ProductionOrderLineValidator());
        }

        private class ProductionOrderLineValidator : AbstractValidator<ProductionOrderLineRequest>
        {
            public ProductionOrderLineValidator()
            {
                RuleFor(line => line.ProductId).NotEmpty();
                RuleFor(line => line.TotalBatches).GreaterThan(0);
                RuleFor(line => line.QuantityPerBatch).GreaterThan(0);
                RuleFor(line => line.MeasurementUnitId).NotEmpty();
            }
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
                    "CreateProductionOrder.Validation", validationResult.ToString()));
            }

            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            //try
            //{
                var productionOrder = new ProductionOrder
                {
                    OrderDate = request.OrderDate,
                    ScheduledDate = request.ScheduleDate
                };

                foreach (var line in request.ProductionOrderLines)
                {
                    var productionOrderLine = new ProductionOrderLine
                    {
                        ProductId = line.ProductId,
                        TotalBatches = line.TotalBatches,
                        QuantityPerBatch = line.QuantityPerBatch,
                        MeasurementUnitId = line.MeasurementUnitId,
                        StartDate = line.StartDate,
                        ExpirationDate = line.ExpirationDate
                    };

                    productionOrder.ProductionOrderLines.Add(productionOrderLine);
                }
                _context.ProductionOrders.Add(productionOrder);
                await _context.SaveChangesAsync(cancellationToken);

                var productionOrderLines = productionOrder.ProductionOrderLines.ToList();

                var productionOfDayCount = await _context.ProductionOrders
                         .Where(x => EF.Functions.DateDiffDay(x.OrderDate, request.OrderDate)==0)
                         .CountAsync(cancellationToken);

                foreach (var line in productionOrderLines)
                {
                    for (int i = 0; i < line.TotalBatches; i++)
                    {

                        var batchExists = true;
                        var batchNumber = string.Empty;
                        while (batchExists)
                        {
                            batchNumber = $"{request.OrderDate:ddMMyy}-{productionOfDayCount + 1}";
                            batchExists = await _context.ProductionOrderLineDetails
                                .AnyAsync(x => x.BatchNumber == batchNumber, cancellationToken);
                            productionOfDayCount++;
                        }
                        var lineDetail = new ProductionOrderLineDetail
                        {
                            BatchNumber = batchNumber
                        };
                        line.ProductionOrderLineDetails.Add(lineDetail);
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                }

                //generate product inventory and raw material requirements for each production order line
                foreach(var line in productionOrderLines)
                {
                  
                 
                        
                        foreach (var lineDetail in line.ProductionOrderLineDetails)
                        {
                            //generate product inventory
                            var productInventory = new ProductInventory
                            {
                                InventoryId = lineDetail.InventoryId,
                                ProductId = line.ProductId,
                                MeasurementUnitId = line.MeasurementUnitId,
                                Quantity = line.QuantityPerBatch,
                                BatchNumber = lineDetail.BatchNumber,
                                ProductionDate = line.StartDate,
                                ExpirationDate = line.ExpirationDate,
                                Flag = 5 //In production
                            };
                            _context.ProductInventories.Add(productInventory);

                            //generate raw material requirements
                            var bom = await _context.BillOfMaterials
                                .Where(x => x.ProductId == line.ProductId)
                                .ToListAsync(cancellationToken);
                            foreach (var item in bom)
                            {
                                var orderLineDetailResource = new ProductionOrderLineDetailResource
                                {
                                    InventoryId = lineDetail.InventoryId,
                                    RawMaterialId = item.RawMaterialId,
                                    Quantity = item.RawMaterialQuantity * line.QuantityPerBatch,
                                    MeasurementUnitId = item.RawMaterialMeasurementUnitId
                                };
                                _context.ProductionOrderLineDetailResources.Add(orderLineDetailResource);
                            }
                        }
                    
                }

                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return Result<Guid>.Success(productionOrder.Id);
            //} catch (Exception ex)
            //{
                 
            //    return Result.Failure<Guid>(new Error("CreateProductionOrder.Error", $"{ex.Message}\n\n{ex}"));
            //}    
        }
    }

}

public class CreateProductionOrderEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/ProductionOrder", async (ProductionOrderRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductionOrder.Command>();
            var result = await sender.Send(command);
            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        });
    }
}
