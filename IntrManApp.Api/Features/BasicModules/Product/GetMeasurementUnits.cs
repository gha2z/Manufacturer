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

public static class GetMeasurementUnits
{
    public class Query : IRequest<Result<List<MeasurementUnitRequest>>>
    {

    }

    internal sealed class Handler(IntrManDbContext context) : IRequestHandler<Query, Result<List<MeasurementUnitRequest>>>
    {
        
        public async Task<Result<List<MeasurementUnitRequest>>> Handle(Query request, CancellationToken cancellationToken)
        {
            if (!context.MeasurementUnitGroups.Any())
            {
                var measurementUnitGroup = new MeasurementUnitGroup
                {
                    Name = "Weight - Kgs"
                };

                var childUnit = new MeasurementUnit()
                {
                    Name = "gram",
                    Quantity = 1,
                    Initial = "g",
                    Group = measurementUnitGroup
                };

                var parentUnit = new MeasurementUnit()
                {
                    Name = "kilogram",
                    Quantity = 1000,
                    Child = childUnit,
                    Initial = "kg",
                    Group = measurementUnitGroup
                };
                context.Add(measurementUnitGroup);
                context.Add(childUnit);
                context.Add(parentUnit);

                await context.SaveChangesAsync(cancellationToken);
            }

            var measurementUnits = await context.MeasurementUnits.ToListAsync(cancellationToken);
            foreach(var measurementUnit in measurementUnits)
            {
                if(string.IsNullOrEmpty(measurementUnit.Initial))
                {
                    measurementUnit.Initial = measurementUnit.Name.ToLower().Equals("gram") ? "g" :
                        measurementUnit.Name.ToLower().Equals("kilogram") ? "kg" : "";
                    context.MeasurementUnits.Update(measurementUnit);
                    await context.SaveChangesAsync(cancellationToken);
                }
            }
            List<MeasurementUnitRequest>? queryResults =
                measurementUnits.Adapt<List<MeasurementUnitRequest>>();

            if (queryResults == null)
            {
                return Result.Failure<List<MeasurementUnitRequest>>(new Error("GetMeasurementUnits.NotFound", "No ProductCategories found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetMeasurementUnitsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/measurementUnits", async (ISender sender) =>
        {
            var query = new GetMeasurementUnits.Query();

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
