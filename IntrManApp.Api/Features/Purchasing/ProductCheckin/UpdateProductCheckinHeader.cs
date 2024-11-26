using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using MediatR;

namespace IntrManApp.Api.Features.Purchasing.MaterialCheckin
{
    public class UpdateProductCheckinHeader
    {
        public class Command : IRequest<Result<Guid>>
        {
            public Guid Id { get; set; }
            public Guid SupplierId { get; set; }
            public DateTime CheckInDate { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.SupplierId).NotEmpty();
                RuleFor(c => c.CheckInDate).NotEmpty();
            }
        }

        internal sealed class Handler: IRequestHandler<Command, Result<Guid>>
        {
            private readonly Gha2zErpDbContext _context;
            private readonly IValidator<Command> _validator;

            public Handler(Gha2zErpDbContext dbContext, IValidator<Command> validator)
            {
                _context = dbContext;
                _validator = validator;
            }

            public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = _validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return Result.Failure<Guid>(new Error("UpdateProductCheckin.Validation", validationResult.ToString()));
                }

             
                var productCheckin = await _context.ProductCheckIns
                    .FindAsync([request.Id, cancellationToken], cancellationToken: cancellationToken);

                if (productCheckin == null)
                {
                    return Result.Failure<Guid>(new Error("UpdateProductCheckin.NotFound", $"Product checkin with id {request.Id} not found"));
                }

                try
                { 
                    productCheckin.SupplierId = request.SupplierId;
                    productCheckin.CheckInDate = request.CheckInDate;

                    await _context.SaveChangesAsync(cancellationToken);
                    _context.Database.CommitTransaction();

                    return Result.Success(productCheckin.Id);
                }
                catch (Exception ex)
                {
                    return Result.Failure<Guid>(new Error("UpdateProductCheckin.Error", ex.Message));
                }
            }
        }
    }

    public class UpdateProductCheckinHeaderEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("api/ProductCheckinHeaders", 
                async (ProductCheckinRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCheckinHeader.Command>();
                var result =  await sender.Send(command);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }
                return Results.Ok(result.Value);
            }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
            {
                Description = "Modifies raw materials checkin in header and returns the new created supplier id on successful operation",
                Summary = "Update Raw Material Checkin Header",
                Tags =
                [
                    new() {
                        Name = "Raw Materials Checkin"
                    }
                ]
            });
        }
    }
}
