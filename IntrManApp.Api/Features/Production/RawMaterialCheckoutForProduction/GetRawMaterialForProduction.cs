﻿using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.BasicModules;

public static class GetRawMaterialsForProduction
{
    public class Query : IRequest<Result<IEnumerable<InventoryItemDetail>>>
    {

    }

    internal sealed class Handler(IDbConnectionFactory dbConnectionFactory) :
    IRequestHandler<Query, Result<IEnumerable<InventoryItemDetail>>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<Result<IEnumerable<InventoryItemDetail>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            IEnumerable<InventoryItemDetail>? queryResults =
                (IEnumerable<InventoryItemDetail>?)await connection.QueryAsync<InventoryItemDetail>(
                    "Production.GetRawMaterialsForProduction", commandType: CommandType.StoredProcedure);

            if (queryResults == null)
            {
                return Result.Failure<IEnumerable<InventoryItemDetail>>(new Error("GetRawMaterialsForProduction.NotFound", "No products found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetRawMaterialsForProductionEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/rawMaterialsForProduction", async (ISender sender) =>
        {
            var query = new GetRawMaterialsForProduction.Query();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show list of Raw Materials for Production",
            Summary = "Raw Materials For Production",
            Tags =
            [
                new Microsoft.OpenApi.Models.OpenApiTag
                {
                    Name = "Product"
                }
            ]
        });
    }
}
