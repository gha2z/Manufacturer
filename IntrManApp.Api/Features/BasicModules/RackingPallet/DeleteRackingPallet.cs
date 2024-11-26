using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using MediatR;

namespace IntrManApp.Api.Features.BasicModules
{
    public class DeleteRackingPallet
    {
        public class Command : IRequest<Result<bool>>
        {
            public Guid Id { get; set; }
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
                var RackingPallet = await _context.RackingPallets
                    .FindAsync(request.Id, cancellationToken);
                if (RackingPallet != null)
                {
                    _context.RackingPallets.Remove(RackingPallet);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return Result.Failure<bool>(new Error(
                      "DeleteRackingPallet.Validation", "Item not found"));
                }
            }
        }
    }

    public class DeleteRackingPalletEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/rackingPallets/{id}",
                async (Guid id, ISender sender) =>
                {
                    var command = new DeleteRackingPallet.Command { Id = id };
                    var result = await sender.Send(command);

                    if (result.IsFailure)
                    {
                        return Results.BadRequest(result.Error);
                    }
                    return Results.Ok(result.Value);
                }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
                {
                    Description = "Delete the existing RackingPallet and returns true on succesful operation",
                    Summary = "Delete RackingPallet",
                    Tags = new List<Microsoft.OpenApi.Models.OpenApiTag>
                {
                    new Microsoft.OpenApi.Models.OpenApiTag
                    {
                        Name = "RackingPallet"
                    }
                }
                });
        }
    }
}
