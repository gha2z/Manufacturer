using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.BasicModules;

public static class GetCustomers
{
    public class Query : IRequest<Result<List<CustomerResponse>>>
    {

    }

    internal sealed class Handler : IRequestHandler<Query, Result<List<CustomerResponse>>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public Handler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<Result<List<CustomerResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            List<CustomerResponse>? queryResults =
                (List<CustomerResponse>?)await connection.QueryAsync<CustomerResponse>(
                    "Sales.CustomerList", commandType: CommandType.StoredProcedure);

            if (queryResults == null)
            {
                return Result.Failure<List<CustomerResponse>>(new Error("GetCustomers.NotFound", "No Customers found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetCustomersEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/customers", async (ISender sender) =>
        {
            var query = new GetCustomers.Query();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show list of Customers",
            Summary = "Customer List",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag>
            {
                new Microsoft.OpenApi.Models.OpenApiTag
                {
                    Name = "Customer"
                }
            }
        });
    }
}
