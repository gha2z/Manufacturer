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
    public static class CreateLocation
    {
        public class Command : IRequest<Result<Guid>>
        {
            public Guid Id { get; set; } = Guid.Empty;
            public string Name { get; set; } = string.Empty;
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Name).NotEmpty();
            }
        }

        internal sealed class Handler(Gha2zErpDbContext dbContext, IValidator<CreateLocation.Command> validator) : IRequestHandler<Command, Result<Guid>>
        {
            private readonly Gha2zErpDbContext _context = dbContext;
            private readonly IValidator<Command> _validator = validator;

            public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = _validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return Result.Failure<Guid>(new Error(
                        "CreateLocation.Validation", validationResult.ToString()));
                }
                var location = new Location
                {
                    Name = request.Name
                };
                _context.Add(location);
                await _context.SaveChangesAsync();
                return location.Id;
            }
        }
    }

    public class CreateLocationEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/locations", async (LocationRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateLocation.Command>();
                var result = await sender.Send(command);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }
                return Results.Ok(result.Value);
            }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
            {
                Description = "Creates new location and returns the new location id",
                Summary = "Create location",
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
