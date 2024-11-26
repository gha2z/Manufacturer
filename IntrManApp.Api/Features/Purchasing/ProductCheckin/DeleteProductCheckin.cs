using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Carter;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace IntrManApp.Api.Features.Purchasing.MaterialCheckin
{
    public static class DeleteProductCheckin
    {
        public class Command : IRequest<Result<bool>>
        {
            public Guid CheckinId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(checkin => checkin.CheckinId).NotEmpty();
            }
        }

        internal sealed class Handler : IRequestHandler<Command, Result>
        {
            private readonly Gha2zErpDbContext _context;
            private readonly IValidator<Command> _validator;

            public Handler(Gha2zErpDbContext dbContext, IValidator<Command> validator)
            {
                _context = dbContext;
                _validator = validator;
            }
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = _validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return Result.Failure<bool>(new Error(
                           "DeleteProductcheckin.Validation", validationResult.ToString()));
                }

                var checkin = await _context.ProductCheckIns
                    .FindAsync(request.CheckinId, cancellationToken);

                if (checkin == null)
                {
                    return Result.Failure<bool>(
                        new Error("DeleteProductCheckIn.Validation","Checkin not found"));
                }

                try
                {
                    _context.ProductInventories
                        .Where(p => p.TransIdReference.Equals(request.CheckinId))
                        .ExecuteDelete();

                    _context.ProductCheckIns.Remove(checkin);
                    await _context.SaveChangesAsync(cancellationToken);

                    return Result.Success(true);
                } catch (Exception ex)
                {
                    return Result.Failure<bool>(new Error(
                                               "DeleteProductCheckin.Validation", $"{ex.Message}\n\n{ex}"));
                }

              
            }
        }
    }

    public class DeleteProductCheckInEndiPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/api/productcheckins/{id}", async (Guid id, ISender sender) =>
            {
                var command = new DeleteProductCheckin.Command() { CheckinId = id };

                var result = await sender.Send(command);

                if (result.IsSuccess)
                {
                    return Results.Ok(result.Value);
                }
                else
                {
                    return Results.BadRequest(result.Error);
                }
            }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
            {
                Description = "Delete Raw Materials checkin in and " +
                    "returns TRUE on successful operation." +
                    "This will automatically remove the relevant product inventories.",
                Summary = "Delete raw materials check-in",
                Tags = new List<Microsoft.OpenApi.Models.OpenApiTag>
                {
                    new Microsoft.OpenApi.Models.OpenApiTag
                    {
                        Name = "Raw Materials Checkin"
                    }
                }
            });
        }
    }
}
