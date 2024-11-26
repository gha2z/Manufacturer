using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using MediatR;

namespace IntrManApp.Api.Features.BasicModules
{
    public class DeleteLocation
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


                var location = await _context.Locations
                    .FindAsync(request.Id, cancellationToken);
                if (location != null)
                {
                    _context.Locations.Remove(location);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return Result.Failure<bool>(new Error(
                      "DeleteLocation.Validation", "Item not found"));
                }
            }
        }
    }

    public class DeleteLocationEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/locations/{id}",
                async (Guid id, ISender sender) =>
                {
                    var command = new DeleteLocation.Command { Id = id };
                    var result = await sender.Send(command);

                    if (result.IsFailure)
                    {
                        return Results.BadRequest(result.Error);
                    }
                    return Results.Ok(result.Value);
                }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
                {
                    Description = "Delete the existing location and returns true on succesful operation",
                    Summary = "Delete location",
                    Tags = new List<Microsoft.OpenApi.Models.OpenApiTag>
                {
                    new Microsoft.OpenApi.Models.OpenApiTag
                    {
                        Name = "Location"
                    }
                }
                });
        }
    }
}
