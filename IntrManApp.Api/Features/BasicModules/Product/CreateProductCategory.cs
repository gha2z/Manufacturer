using Carter;
using FluentValidation;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Models.Production;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using Mapster;

namespace IntrManApp.Api.Features.BasicModules
{
    public static class CreateProductCategory
    {
        public class Command : IRequest<Result<Guid>>
        {
            public string Name { get; set; } = string.Empty;
        }

        public class Validator: AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c=>c.Name).NotEmpty();
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
                        "CreateProductCategory.Validation", validationResult.ToString()));
                }
                var category = new ProductCategory 
                { 
                    Name = request.Name,
                    ModifiedDate = DateTime.Now
                };
                _context.Add(category);
                await _context.SaveChangesAsync();
                return category.Id;
            }
        }
    }

    public class CreateProductCategoryEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/createProductCategory", async (CreateProductCategoryRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCategory.Command>();
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
