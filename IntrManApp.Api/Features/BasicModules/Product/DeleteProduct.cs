using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using MediatR;

namespace IntrManApp.Api.Features.BasicModules
{
    public class DeleteProduct
    {
        public class Command : IRequest<Result<bool>>
        {
            public Guid Id { get; set; }
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


                var item = _context.Products
                    .Where(c => c.Id.Equals(request.Id)).First();
                if (item != null)
                {
                    _context.Products.Remove(item);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return Result.Failure<bool>(new Error(
                      "DeleteProduct.Validation", "Item not found"));
                }
            }
        }
    }

    public class DeleteProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/deleteProduct",
                async (DeleteProductRequest request, ISender sender) =>
                {
                    var command = request.Adapt<DeleteProduct.Command>();
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
