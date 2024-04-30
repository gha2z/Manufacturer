using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.BasicModules;

public static class GetSupplierById
{
    public class Query : IRequest<Result<SupplierResponse>>
    {
        public Guid BusinessEntityId { get; set; }
    }

    internal sealed class Handler : IRequestHandler<Query, Result<SupplierResponse>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public Handler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<Result<SupplierResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            SupplierResponse? queryResults =
                (SupplierResponse?)await connection.QueryFirstOrDefaultAsync<SupplierResponse>(
                    "Purchasing.GetSupplierById", param: new {  request.BusinessEntityId }, commandType: CommandType.StoredProcedure);

            if (queryResults == null)
            {
                return Result.Failure<SupplierResponse>(new Error("GetSuppliers.NotFound", "No Suppliers found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetSupplierByIdEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/suppliers/{id}", async (Guid id, ISender sender) =>
        {
            var query = new GetSupplierById.Query() { BusinessEntityId = id };

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Get a supplier by BusinessEntityId",
            Summary = "Get a supplier",
            Tags =
            [
                new() {
                    Name = "Supplier"
                }
            ]
        });
    }
}
