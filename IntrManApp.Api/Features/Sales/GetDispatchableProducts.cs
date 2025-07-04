﻿using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.BasicModules;

public static class GetDispatchableProducts
{
    public class Query : IRequest<Result<IEnumerable<EndProductItemDetail>>>
    {

    }

    internal sealed class Handler(IDbConnectionFactory dbConnectionFactory) :
    IRequestHandler<Query, Result<IEnumerable<EndProductItemDetail>>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<Result<IEnumerable<EndProductItemDetail>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            IEnumerable<EndProductItemDetail>? queryResults =
                (IEnumerable<EndProductItemDetail>?)await connection.QueryAsync<EndProductItemDetail>(
                    "Production.GetDispatchableProducts", commandType: CommandType.StoredProcedure);

            if (queryResults == null)
            {
                return Result.Failure<IEnumerable<EndProductItemDetail>>(new Error("GetDispatchableProducts.NotFound", "No products found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetDispatchableProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/dispatchableProducts", async (ISender sender) =>
        {
            var query = new GetDispatchableProducts.Query();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show list of dispatchable produts",
            Summary = "Dispatchable Products",
            Tags =
            [
                new Microsoft.OpenApi.Models.OpenApiTag
                {
                    Name = "Sales"
                }
            ]
        });
    }
}
