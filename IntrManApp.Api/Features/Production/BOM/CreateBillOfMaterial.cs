using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using IntrManApp.Api.Entities;
using Mapster;
using MediatR;

namespace IntrManApp.Api.Features.Production
{
    public static class CreateBom
    {
        public class Command : IRequest<Result<bool>>
        {
            public Guid ProductId { get; set; }
            public List<BomSpecification> BomSpecifications { get; set; } = [];
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.ProductId).NotEmpty();
                RuleForEach(bom => bom.BomSpecifications).SetValidator(new BomSpecificationValidator());
            }
        }

        private class BomSpecificationValidator : AbstractValidator<BomSpecification>
        {
            public BomSpecificationValidator()
            {
                RuleFor(spec => spec.RawMaterialId).NotEmpty();
                RuleFor(spec => spec.RowMaterialQuantity).GreaterThan(0);
                RuleFor(spec => spec.RawMaterialMeasurementUnitId).NotEmpty();
            }
        }

        internal sealed class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly IntrManDbContext _context;
            private readonly IValidator<Command> _validator;

            public Handler(IntrManDbContext dbContext, IValidator<Command> validator)
            {
                _context = dbContext;
                _validator = validator;
            }
            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = _validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return Result.Failure<bool>(new Error(
                        "CreateBom.Validation", validationResult.ToString()));
                }

                try
                {
                    foreach(var bom in request.BomSpecifications)
                    {
                        BillOfMaterial? billOfMaterial;
                        if (bom.Id == Guid.Empty)
                        {
                            billOfMaterial = new();
                            _context.BillOfMaterials.Add(billOfMaterial);
                        }
                        else
                        {
                            billOfMaterial = await _context.BillOfMaterials.FindAsync(
                            [bom.Id], cancellationToken: cancellationToken);
                        }
                        if (billOfMaterial != null) 
                        { 
                            billOfMaterial.ProductId = request.ProductId;
                            billOfMaterial.RawMaterialId = bom.RawMaterialId;
                            billOfMaterial.RawMaterialMeasurementUnitId = bom.RawMaterialMeasurementUnitId;
                            billOfMaterial.RawMaterialQuantity = bom.RowMaterialQuantity;
                        };
                    }
                    await _context.SaveChangesAsync(cancellationToken);

                    return true;
                }
                catch (Exception ex)
                {
                    return Result.Failure<bool>(new Error(
                       "CreateBom.Validation", $"{ex.Message}\n\n{ex}"));
                }
            }
        }
    }

    public class CreateBomEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/boms", async (BomRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateBom.Command>();

                var result = await sender.Send(command);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }
                return Results.Ok(result.Value);
            }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
            {
                Description = "Defines bill of maaterials to manufacture an end product and returns TRUE on successful operation",
                Summary = "Defines bill of materials",
                Tags = new List<Microsoft.OpenApi.Models.OpenApiTag>
                {
                    new Microsoft.OpenApi.Models.OpenApiTag
                    {
                        Name = "Bill of Materials"
                    }
                }
            });
        }

    }
}
