using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using MediatR;

namespace IntrManApp.Api.Features.BasicModules
{
    public class DeleteCustomer
    {
        public class Command : IRequest<Result<bool>>
        {
            public Guid BusinessEntityId { get; set; }
        }

        internal sealed class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly IntrManDbContext _context;

            public Handler(IntrManDbContext dbContext)
            {
                _context = dbContext;
            }
            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {


                var item = _context.Customers
                    .Where(c => c.BusinessEntityId.Equals(request.BusinessEntityId)).First();
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
            app.MapPost("api/deleteCustomer",
                async (DeleteCustomerRequest request, ISender sender) =>
                {
                    var command = request.Adapt<DeleteCustomer.Command>();
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
