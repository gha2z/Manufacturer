using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using MediatR;

namespace IntrManApp.Api.Features.BasicModules
{
    public class DeleteSupplier
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


                var item = _context.Suppliers
                    .Where(c => c.BusinessEntityId.Equals(request.BusinessEntityId)).FirstOrDefault();
                if (item != null)
                {
                    _context.Suppliers.Remove(item);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return Result.Failure<bool>(new Error(
                      "DeleteSupplier.Validation", "Item not found"));
                }
            }
        }
    }

    public class DeleteSupplierEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/deleteSupplier",
                async (DeleteSupplierRequest request, ISender sender) =>
                {
                    var command = request.Adapt<DeleteSupplier.Command>();
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
