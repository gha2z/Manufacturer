using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IntrManApp.Api.Features.Production.ProductionOrder
{

    public static class NextDispatchStatus
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
                        "NextDispatchStatus.Validation", validationResult.ToString()));
                }

                    using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

                    try
                    {
                        var inventoryItem = await _context.ProductInventories
                            .FindAsync(request.Id, cancellationToken);

                        if (inventoryItem != null && inventoryItem.Flag<12)
                        {
                            inventoryItem.Flag++;
                            inventoryItem.ModifiedDate = DateTime.Now;
                        }
                        await _context.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync(cancellationToken);
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
            app.MapGet("api/nextDispatchStatus/{id}", async (Guid id, ISender sender) =>
            {
                var command = new NextDispatchStatus.Command() { Id = id };

                var result = await sender.Send(command);

                if (result.IsSuccess)
                {
                    return Results.Ok(result.Value);
                }

                return Results.BadRequest(result.Error);
            }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
            {
                Description = "Set next dispatch status for an inventory item and " +
                    "returns TRUE on successful operation." +
                    "10 (",
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
