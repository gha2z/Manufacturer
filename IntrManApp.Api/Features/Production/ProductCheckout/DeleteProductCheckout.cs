using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Api.Entities;
using IntrManApp.Shared.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace IntrManApp.Api.Features.Production.MaterialCheckout
{
    public static class DeleteProductCheckout
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
                        "DeleteProductCheckout.Validation", validationResult.ToString()));
                }

                try
                {
                    var productCheckout = await _context.ProductCheckouts
                        .FindAsync(request.Id, cancellationToken);

                    if (productCheckout == null)
                    {
                        return Result.Failure<bool>(
                            new Error("DeleteProductCheckout.NotFound", $"Product checkout with id {request.Id} not found"));
                    }

                    try
                    {
                        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

                        // update ProductInventory to the source location and racking pallet
                        FormattableString sql = $@"
                            UPDATE Production.ProductInventory i 
                            SET i.LocationId = l.SourceLocationId, i.RackingPalletId = l.SourceRackingPalletId
                            INNER JOIN Poduction.ProductCheckOutLine l 
                            ON i.InventoryId = l.InventoryId 
                            WHERE l.CheckOutId='{request.Id}'";
                        
                        await _context.Database.ExecuteSqlAsync(sql, cancellationToken);

                        _context.ProductCheckouts.Remove(productCheckout);

                        await _context.SaveChangesAsync(cancellationToken);

                        await _context.Database.CommitTransactionAsync(cancellationToken);
                        return Result.Success(true);
                    }
                    catch (Exception ex)
                    {
                        await _context.Database.RollbackTransactionAsync(cancellationToken);
                        return Result.Failure<bool>(new Error("DeleteProductCheckout.Error", ex.Message));
                    }
                }
                catch (Exception ex)
                {
                    return Result.Failure<bool>(new Error("DeleteProductCheckout.Error", ex.Message));
                }
            }
        }
    }
}
