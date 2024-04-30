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
    public static class CreateRackingPallet
    {
        public class Command : IRequest<Result<Guid>>
        {
            public Guid Id { get; set; } = Guid.Empty;
            public string Col { get; set; } = string.Empty;
            public short Row { get; set; }
            public string Description { get; set; } = string.Empty; 
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Col).NotEmpty();
            }
        }

        internal sealed class Handler : IRequestHandler<Command, Result<Guid>>
        {
            private readonly IntrManDbContext _context;
            private readonly IValidator<Command> _validator;

            public Handler(IntrManDbContext dbContext, IValidator<Command> validator)
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
                        "CreateRackingPallet.Validation", validationResult.ToString()));
                }
                var RackingPallet = new RackingPallet
                {
                    Col = request.Col,
                    Row = request.Row,
                    Description = request.Description
                };

                _context.Add(RackingPallet);
                await _context.SaveChangesAsync();
                return RackingPallet.Id;
            }
        }
    }

    public class CreateRackingPalletEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/rackingPallets", async (RackingPalletRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateRackingPallet.Command>();
                var result = await sender.Send(command);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }
                return Results.Ok(result.Value);
            }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
            {
                Description = "Creates new RackingPallet and returns the new RackingPallet id",
                Summary = "Create RackingPallet",
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
