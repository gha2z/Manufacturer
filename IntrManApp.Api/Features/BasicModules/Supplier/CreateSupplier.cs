using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using Mapster;
using IntrManApp.Api.Entities;

namespace IntrManApp.Api.Features.BasicModules
{
    public static class CreateSupplier
    {
        public class Command : IRequest<Result<Guid>>
        {
            public Guid BusinessEntityId { get; set; } = Guid.Empty;
            public string Name { get; set; } = string.Empty;
            public bool IsActive { get; set; } = true; 
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
                        "CreateSupplier.Validation", validationResult.ToString()));
                }
                Supplier item;
                BusinessEntity entity;
                if(request.BusinessEntityId == Guid.Empty)
                {
                    entity = new();
                    _context.Add(entity);
                    await _context.SaveChangesAsync();
                } else
                {
                    entity = _context.BusinessEntities
                        .Where(b => b.Id.Equals(request.BusinessEntityId)).FirstOrDefault();
                }
                if (entity != null)
                {
                    item = new()
                    {
                        BusinessEntityId = entity.Id,
                        Name = request.Name,
                        IsActive = request.IsActive
                    };
                    _context.Add(item);
                    await _context.SaveChangesAsync();
                    return item.BusinessEntityId;
                } else
                {
                    return Result.Failure<Guid>(new Error(
                      "CreateSupplier.Validation", "Business Entity not found"));
                }
            }
        }
    }

    public class CreateSupplierEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/suppliers", async (CreateSupplierRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateSupplier.Command>();
                var result = await sender.Send(command);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }
                return Results.Ok(result.Value);
            }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
            {
                Description = "Creates a new supplier and returns the new created supplier id on successful operation",
                Summary = "Create a new supplier",
                Tags = new List<Microsoft.OpenApi.Models.OpenApiTag>
                {
                    new Microsoft.OpenApi.Models.OpenApiTag
                    {
                        Name = "Supplier"
                    }
                }
            });
        }

    }
}
