using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Api.Entities;
using MediatR;

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
            private readonly IntrManDbContext _context;
            private readonly IValidator<Command> _validator;

            public Handler(IntrManDbContext dbContext, IValidator<Command> validator)
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

                _context.ProductCheckIns.Remove(checkin);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success(true);
            }
        }
    }
}
