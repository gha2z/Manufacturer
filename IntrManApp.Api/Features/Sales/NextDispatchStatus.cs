using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IntrManApp.Api.Features.Production.ProductionOrder
{

    public static class NextDispatchStatus
    {
        public class Command : IRequest<Result<bool>>
        {
            public Guid OrderId { get; set; }
            public Guid InventoryId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.OrderId).NotEmpty();
            }
        }

        internal sealed class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly Gha2zErpDbContext _context;
            private readonly IValidator<Command> _validator;

            public Handler(Gha2zErpDbContext dbContext, IValidator<Command> validator)
            {
                _context = dbContext;
                _validator = validator;
            }

            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = _validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return Result.Failure<bool>(new Error(
                        "NextDispatchStatus.Validation", validationResult.ToString()));
                }

                    using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

                    try
                    {

                        var orders = await _context.SalesOrderLines
                            .FirstOrDefaultAsync(
                                x => x.OrderId == request.OrderId && x.InventoryId == request.InventoryId, cancellationToken);

                        var inventoryItem = await _context.ProductInventories
                            .FindAsync(request.InventoryId, cancellationToken);

                        if (inventoryItem != null && orders != null)
                        {
                            orders.Flag++;
                            if (orders.Flag == 12)
                            {
                                inventoryItem.Reserved -= orders.Quantity;
                                inventoryItem.TotalBatches -= orders.Quantity;
                                inventoryItem.ModifiedDate = DateTime.Now;
                            }
                            await _context.SaveChangesAsync(cancellationToken);
                            await transaction.CommitAsync(cancellationToken);

                        }
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync(cancellationToken);
                        return Result.Failure<bool>(new Error("NextDispatchStatus.Validation", $"{ex.Message}\n\n{ex}"));
                    }

                    return Result.Success(true);
            }
        }
    }

    public class NextDispatchStatusEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/nextDispatchStatus", async (NextDispatchStatusRequest request, ISender sender) =>
            {
                var command = new NextDispatchStatus.Command() 
                { 
                    OrderId = request.OrderId,
                    InventoryId = request.InventoryId
                };

                var result = await sender.Send(command);

                if (result.IsSuccess)
                {
                    return Results.Ok(result.Value);
                }

                return Results.BadRequest(result.Error);
            }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
            {
                Description = "Set next dispatch status for a dispatch order line " +
                    "returns TRUE on successful operation." +
                    "10 (",
                Summary = "Set Next Dispatch Status",
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
