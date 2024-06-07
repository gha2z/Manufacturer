using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.BasicModules;

public static class GetDispatchOrderDetailByDate
{
    public class Query : IRequest<Result<IEnumerable<DispatchOrderDetail>>>
    {
        public DateTime Date { get; set; }
    }

    internal sealed class Handler(IDbConnectionFactory dbConnectionFactory) :
    IRequestHandler<Query, Result<IEnumerable<DispatchOrderDetail>>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<Result<IEnumerable<DispatchOrderDetail>>>Handle(
            Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            IEnumerable<DispatchOrderDetail>? queryResults =
                (IEnumerable<DispatchOrderDetail>?)await connection.QueryAsync<DispatchOrderDetail>(
                    "Sales.GetDispatchOrderDetailByDate",  new { request.Date } ,  commandType: CommandType.StoredProcedure);

            if (queryResults == null)
            {
                return Result.Failure<IEnumerable<DispatchOrderDetail>>(
                    new Error("GetDispatchOrderDetailByDate.NotFound", "No products found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class GetDispatchOrderDetailByDateEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/dispatchOrderDetailByDate/{date}", async (DateTime date, ISender sender) =>
        {
            var query = new GetDispatchOrderDetailByDate.Query() { Date = date };

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show list of dispatch order detail by date",
            Summary = "Dispatch order detail by date",
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
