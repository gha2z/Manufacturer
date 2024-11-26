using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Api.Entities;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using Mapster;

namespace IntrManApp.Api.Features.BasicModules
{
    public static class UpdateCustomer
    {
        public class Command : IRequest<Result<Guid>>
        {
            public Guid BusinessEntityId { get; set; }
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
                    return Result.Failure<Guid>(new Error(
                        "UpdateCustomer.Validation", validationResult.ToString()));
                }
                var item = _context.Customers
                    .Where(c => c.BusinessEntityId.Equals(request.BusinessEntityId)).FirstOrDefault();
                if (item != null)
                {
                    item.BusinessEntityId = request.BusinessEntityId;
                    item.Name = request.Name;
                    item.IsActive = request.IsActive;
                    await _context.SaveChangesAsync();
                    return item.BusinessEntityId;
                }
                else
                {
                    return Result.Failure<Guid>(new Error(
                      "UpdateCustomer.Validation", "Item not found"));
                }
            }
        }
    }

    public class UpdateCustomerEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("api/customers/",
                async (Customer request, ISender sender) =>
                {
                    var command = request.Adapt<UpdateCustomer.Command>();
                    var result = await sender.Send(command);

                    if (result.IsFailure)
                    {
                        return Results.BadRequest(result.Error);
                    }
                    return Results.Ok(result.Value);
                }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
                {
                    Description = "Update the existing customer and returns the generated customer Id",
                    Summary = "Update customer",
                    Tags = new List<Microsoft.OpenApi.Models.OpenApiTag>
                {
                    new Microsoft.OpenApi.Models.OpenApiTag
                    {
                        Name = "Customer"
                    }
                }
                });
        }

    }
}
