using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using IntrManApp.Api.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
                RuleFor(spec => spec.RawMaterialQuantity).GreaterThan(0);
                RuleFor(spec => spec.RawMaterialMeasurementUnitId).NotEmpty();
            }
        }

        internal sealed class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly Gha2zErpDbContext _context;
            private readonly IValidator<Command> _validator;

            public Handler(Gha2zErpDbContext dbContext, IValidator<Command> validator)
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

                using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var product = await _context.Products
                        .FindAsync([request.ProductId], cancellationToken: cancellationToken);

                    if(product==null) return Result.Failure<bool>(new Error(
                      "CreateBom.Validation", $"Product not found"));

                    _context.BillOfMaterials.Where(b => b.ProductId.Equals(request.ProductId)).ExecuteDelete();
                    //await _context.SaveChangesAsync(cancellationToken);
                    
                    foreach (var bom in request.BomSpecifications)
                    {
                        BillOfMaterial? billOfMaterial = new()
                        {
                            ProductId = request.ProductId,
                            RawMaterialId = bom.RawMaterialId,
                            RawMaterialMeasurementUnitId = bom.RawMaterialMeasurementUnitId,
                            RawMaterialQuantity = bom.RawMaterialQuantity
                        };
                        _context.BillOfMaterials.Add(billOfMaterial);
                    }
                    await _context.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                    return true;
                }

                  

                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
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
