using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Api.Entities;
using IntrManApp.Shared.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IntrManApp.Api.Features.Production.ProductionOrder
{

    public static class AbortItemProduction
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
                                               "AbortItemProduction.Validation", validationResult.ToString()));
                }

                try
                {
                    var productionOrder = await _context.ProductionOrderLineDetails
                        .FindAsync(request.Id, cancellationToken);

                    if (productionOrder == null)
                    {
                        return Result.Failure<bool>(new Error("AbortItemProduction.NotFound", $"Production order with Inventory Id {request.Id} not found"));
                    }

                    using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

                    try
                    {
                        productionOrder.StartDate = null;
                        productionOrder.ModifiedDate = DateTime.Now;

                        var inventoryItem = await _context.ProductInventories
                            .FindAsync(request.Id, cancellationToken);

                        if (inventoryItem != null)
                        {
                            inventoryItem.Flag = 4;
                            inventoryItem.ModifiedDate = DateTime.Now;
                        }

                        await _context.SaveChangesAsync(cancellationToken);

                        await _context.Database.ExecuteSqlInterpolatedAsync(
                       $"Production.DeleteRawMaterialAllocation @inventoryId={request.Id}", cancellationToken: cancellationToken);
                        await transaction.CommitAsync(cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync(cancellationToken);
                        return Result.Failure<bool>(new Error("AbortItemProduction.Validation", $"{ex.Message}\n\n{ex}"));
                    }



                    return Result.Success(true);
                }
                catch (Exception ex)
                {
                    return Result.Failure<bool>(new Error(
                                               "AbortItemProduction.Validation", $"{ex.Message}\n\n{ex}"));
                }
            }
        }
    }

    public class AbortItemProductionEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/AbortItemProduction/{id}", async (Guid id, ISender sender) =>
            {
                var command = new AbortItemProduction.Command() { Id = id };

                var result = await sender.Send(command);

                if (result.IsSuccess)
                {
                    return Results.Ok(result.Value);
                }

                return Results.BadRequest(result.Error);
            }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
            {
                Description = "Abort the production of en end product and " +
                    "returns TRUE on successful operation." +
                    "This will automatically update the product inventory status of the end product from " +
                    "5 (Production - In Progress) to 4 (Production - Aborted).",
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
