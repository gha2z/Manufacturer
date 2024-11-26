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
    public static class UpdateLocation
    {
        public class Command : IRequest<Result<Guid>>
        {
            public Guid Id { get; set; }
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
                        "UpdateLocation.Validation", validationResult.ToString()));
                }
                
                var item = await _context.Locations
                    .FindAsync(request.Id, cancellationToken);

                if (item != null)
                {
                    item.Name = request.Name;
                    await _context.SaveChangesAsync(cancellationToken);
                    return item.Id;
                }
                else
                {
                    return Result.Failure<Guid>(new Error(
                      "UpdateLocation.Validation", "Item not found"));
                }
            }
        }
    }

    public class UpdateLocationEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("api/locations",
                async (LocationRequest request, ISender sender) =>
                {
                    var command = request.Adapt<UpdateLocation.Command>();
                    var result = await sender.Send(command);

                    if (result.IsFailure)
                    {
                        return Results.BadRequest(result.Error);
                    }
                    return Results.Ok(result.Value);
                }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
                {
                    Description = "Update the existing location and returns the location id",
                    Summary = "Update location",
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
