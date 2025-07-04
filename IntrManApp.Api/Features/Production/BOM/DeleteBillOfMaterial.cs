﻿using Carter;
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
    public static class DeleteBom
    {
        public class Command : IRequest<Result<bool>>
        {
            public Guid ProductId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.ProductId).NotEmpty();
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
                        "DeleteBom.Validation", validationResult.ToString()));
                }

                try
                {
                    _context.BillOfMaterials.Where(b => b.ProductId.Equals(request.ProductId)).ExecuteDelete();
                    await _context.SaveChangesAsync(cancellationToken);

                    return true;
                }
                catch (Exception ex)
                {
                    return Result.Failure<bool>(new Error(
                       "DeleteBom.Validation", $"{ex.Message}\n\n{ex}"));
                }
            }
        }
    }

    public class DeleteBomEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/boms/{id}", async (Guid id, ISender sender) =>
            {
                var command = new DeleteBom.Command() { ProductId = id };

                var result = await sender.Send(command);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }
                return Results.Ok(result.Value);
            }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
            {
                Description = "Deletes an existing Bill of Materials for a specified product and returns TRUE on successful operation",
                Summary = "Deletes Bill of Materials",
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
