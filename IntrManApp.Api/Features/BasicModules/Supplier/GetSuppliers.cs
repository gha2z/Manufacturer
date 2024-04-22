using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.BasicModules;

public static class GetSuppliers
{
    public class Query : IRequest<Result<List<SupplierResponse>>>
    {

    }

    internal sealed class Handler : IRequestHandler<Query, Result<List<SupplierResponse>>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public Handler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<Result<List<SupplierResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            List<SupplierResponse>? queryResults =
                (List<SupplierResponse>?)await connection.QueryAsync<SupplierResponse>(
                    "Purchasing.SupplierList", commandType: CommandType.StoredProcedure);

            if (queryResults == null)
            {
                return Result.Failure<List<SupplierResponse>>(new Error("GetSuppliers.NotFound", "No Suppliers found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetSuppliersEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/suppliers", async (ISender sender) =>
        {
            var query = new GetSuppliers.Query();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show list of Suppliers",
            Summary = "Supplier List",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag>
            {
                new Microsoft.OpenApi.Models.OpenApiTag
                {
                    Name = "Supplier"
                }
            }
        });
    }
}
