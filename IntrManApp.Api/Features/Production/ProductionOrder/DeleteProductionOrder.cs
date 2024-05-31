using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IntrManApp.Api.Features.Production.ProductionOrder
{
    public static class DeleteProductionOrder
    {
        public class Command : IRequest<Result<bool>>
        {
            public Guid Id { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Id).NotEmpty();
            }
        } 

        internal sealed class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly IntrManDbContext _context;
            private readonly IValidator<Command> _validator;

            public Handler(IntrManDbContext dbContext, IValidator<Command> validator)
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
                                               "DeleteProductionOrder.Validation", validationResult.ToString()));
                }

                try
                {
                    var productionOrder = await _context.ProductionOrders
                        .FindAsync(request.Id, cancellationToken);

                    if (productionOrder == null)
                    {
                        return Result.Failure<bool>(new Error("DeleteProductionOrder.NotFound", $"Production order with id {request.Id} not found"));
                    }

                    using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

                    try
                    {
                        _context.ProductInventories
                            .Where(p => p.TransIdReference.Equals(request.Id))
                            .ExecuteDelete();

                        _context.ProductionOrders.Remove(productionOrder);

                        await _context.SaveChangesAsync(cancellationToken);
                    } catch (Exception ex)
                    {
                        await transaction.RollbackAsync(cancellationToken);
                        return Result.Failure<bool>(new Error("DeleteProductionOrder.Validation", $"{ex.Message}\n\n{ex}"));
                    }
                    
               

                    return Result.Success(true);
                }
                catch (Exception ex)
                {
                    return Result.Failure<bool>(new Error(
                                               "DeleteProductionOrder.Validation", $"{ex.Message}\n\n{ex}"));
                }
            }
        }
    }

    public class DeleteProductionOrderEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/productionorders/{id}", async (Guid id, ISender sender) =>
            {
                var command = new DeleteProductionOrder.Command() { Id = id };

                var result = await sender.Send(command);

                if (result.IsSuccess)
                {
                    return Results.Ok(result.Value);
                }

                return Results.BadRequest(result.Error);
            }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
            {
                Description = "Delete an existing Production Order and " +
                    "returns TRUE on successful operation." +
                    "This will automatically remove the relevant product inventories.",
                Summary = "Delete an existing Production Order",
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
