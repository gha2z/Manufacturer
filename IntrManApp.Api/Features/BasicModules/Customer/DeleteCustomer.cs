using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using MediatR;

namespace IntrManApp.Api.Features.BasicModules;

public class DeleteCustomer
{
    public class Command : IRequest<Result<bool>>
    {
        public Guid BusinessEntityId { get; set; }
    }

    internal sealed class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly Gha2zErpDbContext _context;

        public Handler(Gha2zErpDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {


            var item = _context.Customers
                .Where(c => c.BusinessEntityId.Equals(request.BusinessEntityId)).FirstOrDefault();
            if (item != null)
            {
                _context.Customers.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return Result.Failure<bool>(new Error(
                  "DeleteCustomer.Validation", "Item not found"));
            }
        }
    }
}

public class DeleteCustomerEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/customers/{id}",
            async (Guid id, ISender sender) =>
            {
                var command = new DeleteCustomer.Command { BusinessEntityId = id };
                var result = await sender.Send(command);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }
                return Results.Ok(result.Value);
            }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
            {
                Description = "Delete the existing customer and returns true on succesful operation",
                Summary = "Delete customer",
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
