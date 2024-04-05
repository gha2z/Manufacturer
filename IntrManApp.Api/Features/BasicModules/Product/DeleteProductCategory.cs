using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using MediatR;

namespace IntrManApp.Api.Features.BasicModules
{
    public class DeleteProductCategory
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
              
             
                var category = _context.ProductCategories
                    .Where(c => c.Id.Equals(request.Id)).First();
                if (category != null)
                {
                    _context.ProductCategories.Remove(category);
                    await _context.SaveChangesAsync();
                    return true;
                } else
                {
                    return Result.Failure<bool>(new Error(
                      "DeleteProductCategory.Validation", "Item not found"));
                }
            }
        }
    }

    public class DeleteProductCategoryEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/deleteProductCategory",
                async (DeleteProductCategoryRequest request, ISender sender) =>
                {
                    var command = request.Adapt<DeleteProductCategory.Command>();
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
