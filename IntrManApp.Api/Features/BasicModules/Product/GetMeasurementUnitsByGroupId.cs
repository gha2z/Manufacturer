using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Api.Entities;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IntrManApp.Api.Features.BasicModules;

public static class GetMeasurementUnitsByGroupId
{
    public class Query : IRequest<Result<List<MeasurementUnitRequest>>>
    {
        public Guid Id { get; set; }
    }

    internal sealed class Handler(Gha2zErpDbContext context) : IRequestHandler<Query, Result<List<MeasurementUnitRequest>>>
    {

        public async Task<Result<List<MeasurementUnitRequest>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var measurementUnits = await context.MeasurementUnits
                .Where(m=>m.GroupId==request.Id).ToListAsync(cancellationToken);
           
            List<MeasurementUnitRequest>? queryResults =
                measurementUnits.Adapt<List<MeasurementUnitRequest>>();

            if (queryResults == null)
            {
                return Result.Failure<List<MeasurementUnitRequest>>(new Error("GetMeasurementUnitsByGroupId.NotFound", "No ProductCategories found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetMeasurementUnitsByGroupIdEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/measurementUnitsByGroupId/{id}", async (Guid id, ISender sender) =>
        {
            var query = new GetMeasurementUnitsByGroupId.Query()
            {
                Id = id
            };

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show list of Product Categories",
            Summary = "Product Cateogry List",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag>
            {
                new Microsoft.OpenApi.Models.OpenApiTag
                {
                    Name = "Product"
                }
            }
        });
    }
}
