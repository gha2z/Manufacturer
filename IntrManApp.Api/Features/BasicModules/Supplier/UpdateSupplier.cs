using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Models.Production;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using Mapster;

namespace IntrManApp.Api.Features.BasicModules
{
    public static class UpdateSupplier
    {
        public class Command : IRequest<Result<Guid>>
        {
            public Guid BusinessEntityId { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Name).NotEmpty();
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
                        "UpdateSupplier.Validation", validationResult.ToString()));
                }
                var item = _context.Suppliers
                    .Where(c => c.BusinessEntityId.Equals(request.BusinessEntityId)).First();
                if (item != null)
                {
                    item.Name = request.Name;
                    await _context.SaveChangesAsync();
                    return item.BusinessEntityId;
                }
                else
                {
                    return Result.Failure<Guid>(new Error(
                      "UpdateSupplier.Validation", "Item not found"));
                }
            }
        }
    }

    public class UpdateSupplierEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/updateSupplier",
                async (UpdateSupplierRequest request, ISender sender) =>
                {
                    var command = request.Adapt<UpdateSupplier.Command>();
                    var result = await sender.Send(command);

                    if (result.IsFailure)
                    {
                        return Results.BadRequest(result.Error);
                    }
                    return Results.Ok(result.Value);
                });
        }

    }
}
