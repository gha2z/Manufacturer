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

public static class GetMeasurementUnitGroups
{
    public class Query : IRequest<Result<List<MeasurementUnitGroupDto>>>
    {

    }

    internal sealed class Handler(Gha2zErpDbContext context) : IRequestHandler<Query, Result<List<MeasurementUnitGroupDto>>>
    {

        public async Task<Result<List<MeasurementUnitGroupDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            MeasurementUnitGroup? measurementUnitGroup = null;
            MeasurementUnit childUnit;
            MeasurementUnit parentUnit;

            if (context.MeasurementUnitGroups.FirstOrDefault(g => g.Name.Equals("Weight - Kgs")) == null)
            {
                measurementUnitGroup = new()
                {
                    Name = "Weight - Kgs"
                };

                childUnit = new()
                {
                    Name = "gram",
                    Quantity = 1,
                    Initial = "g",
                    Group = measurementUnitGroup
                };

                parentUnit = new()
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
            };

            if (context.MeasurementUnitGroups.FirstOrDefault(g => g.Name.Equals("Volume - Litre")) == null)
            {
                measurementUnitGroup = new MeasurementUnitGroup
                {
                    Name = "Volume - Litre"
                };

                childUnit = new MeasurementUnit()
                {
                    Name = "milliliter",
                    Quantity = 1,
                    Initial = "ml",
                    Group = measurementUnitGroup
                };

                parentUnit = new MeasurementUnit()
                {
                    Name = "litre",
                    Quantity = 1000,
                    Child = childUnit,
                    Initial = "l",
                    Group = measurementUnitGroup
                };
                context.Add(measurementUnitGroup);
                context.Add(childUnit);
                context.Add(parentUnit);
            }

            if (context.MeasurementUnitGroups.FirstOrDefault(g => g.Name.Equals("Unit - Piece")) == null)
            {
                measurementUnitGroup = new MeasurementUnitGroup
                {
                    Name = "Unit - Piece"
                };

                childUnit = new MeasurementUnit()
                {
                    Name = "piece",
                    Quantity = 1,
                    Initial = "pc",
                    Group = measurementUnitGroup
                };

                parentUnit = new MeasurementUnit()
                {
                    Name = "dozen",
                    Quantity = 12,
                    Child = childUnit,
                    Initial = "doz",
                    Group = measurementUnitGroup
                };
                context.Add(measurementUnitGroup);
                context.Add(childUnit);
                context.Add(parentUnit);
            }
            if(measurementUnitGroup!=null) await context.SaveChangesAsync(cancellationToken);
            

            var measurementUnitGroups = await context.MeasurementUnitGroups.ToListAsync(cancellationToken);
         
            List<MeasurementUnitGroupDto>? queryResults =
                measurementUnitGroups.Adapt<List<MeasurementUnitGroupDto>>();

            if (queryResults == null)
            {
                return Result.Failure<List<MeasurementUnitGroupDto>>(new Error("GetMeasurementUnitGroups.NotFound", "No Measurement unit groups found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetMeasurementUnitGroupsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/MeasurementUnitGroups", async (ISender sender) =>
        {
            var query = new GetMeasurementUnitGroups.Query();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show list of Measurement Unit Groups",
            Summary = "Measurement Unit Group List",
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
