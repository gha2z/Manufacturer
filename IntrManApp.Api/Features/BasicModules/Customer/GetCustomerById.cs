using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.BasicModules;

public static class GetCustomerById
{
    public class Query : IRequest<Result<CustomerResponse>>
    {
        public Guid BusinessEntityId { get; set; }
    }

    internal sealed class Handler : IRequestHandler<Query, Result<CustomerResponse>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public Handler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<Result<CustomerResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            CustomerResponse? queryResults =
                (CustomerResponse?)await connection.QueryFirstOrDefaultAsync<CustomerResponse>(
                    "Sales.GetCustomerById", param: new { request.BusinessEntityId }, commandType: CommandType.StoredProcedure);

            if (queryResults == null)
            {
                return Result.Failure<CustomerResponse>(new Error("GetCustomers.NotFound", "No Customers found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetCustomerByIdEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/customers/{id}", async (Guid id, ISender sender) =>
        {
            var query = new GetCustomerById.Query() { BusinessEntityId = id };

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Get a Customer by BusinessEntityId",
            Summary = "Get a Customer",
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
