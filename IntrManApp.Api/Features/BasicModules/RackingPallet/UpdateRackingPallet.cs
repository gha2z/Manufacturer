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
    public static class UpdateRackingPallet
    {
        public class Command : IRequest<Result<Guid>>
        {
            public Guid Id { get; set; }
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
                        "UpdateRackingPallet.Validation", validationResult.ToString()));
                }

                var item = await _context.RackingPallets
                    .FindAsync(request.Id, cancellationToken);

                if (item != null)
                {
                    item.Col = request.Col;
                    item.Row = request.Row;
                    item.Description = request.Description;
                    await _context.SaveChangesAsync(cancellationToken);
                    return item.Id;
                }
                else
                {
                    return Result.Failure<Guid>(new Error(
                      "UpdateRackingPallet.Validation", "Item not found"));
                }
            }
        }
    }

    public class UpdateRackingPalletEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("api/rackingPallets",
                async (RackingPalletRequest request, ISender sender) =>
                {
                    var command = request.Adapt<UpdateRackingPallet.Command>();
                    var result = await sender.Send(command);

                    if (result.IsFailure)
                    {
                        return Results.BadRequest(result.Error);
                    }
                    return Results.Ok(result.Value);
                }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
                {
                    Description = "Update the existing RackingPallet and returns the RackingPallet id",
                    Summary = "Update RackingPallet",
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
