using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Models.Purchasing;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using Mapster;
using IntrManApp.Shared.Models.Person;
using IntrManApp.Shared.Models.Sales;

namespace IntrManApp.Api.Features.BasicModules
{
    public static class CreateCustomer
    {
        public class Command : IRequest<Result<Guid>>
        {
            public Guid BusinessEntityId { get; set; } = Guid.Empty;
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
                        "CreateCustomer.Validation", validationResult.ToString()));
                }
                Customer item;
                BusinessEntity entity;
                if (request.BusinessEntityId == Guid.Empty)
                {
                    entity = new();
                    _context.Add(entity);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    entity = _context.BusinessEntities
                        .Where(b => b.Id.Equals(request.BusinessEntityId)).First();
                }
                if (entity != null)
                {
                    item = new()
                    {
                        BusinessEntityId = entity.Id,
                        Name = request.Name,
                        IsActive = true
                    };
                    _context.Add(item);
                    await _context.SaveChangesAsync();
                    return item.BusinessEntityId;
                }
                else
                {
                    return Result.Failure<Guid>(new Error(
                      "CreateCustomer.Validation", "Business Entity not found"));
                }
            }
        }
    }

    public class CreateCustomerEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/createCustomer", async (CreateCustomerRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateCustomer.Command>();
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
