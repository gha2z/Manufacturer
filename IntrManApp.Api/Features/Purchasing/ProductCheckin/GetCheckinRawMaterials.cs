using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.BasicModules;

public static class GetCheckinRawMaterials
{
    public class Query : IRequest<Result<IEnumerable<ProductCheckInLineDetailResponse>>>
    {
        public Guid Id { get; set; }
    }

    internal sealed class Handler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<Query, Result<IEnumerable<ProductCheckInLineDetailResponse>>>
    {
        public async Task<Result<IEnumerable<ProductCheckInLineDetailResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = dbConnectionFactory.CreateOpenConnection();

            IEnumerable<ProductCheckInLineDetailResponse> queryResults =
                (IEnumerable<ProductCheckInLineDetailResponse>)await connection.QueryAsync<ProductCheckInLineDetailResponse>(
                    "Purchasing.GetCheckinRawMaterials", new { request.Id }, commandType: CommandType.StoredProcedure) ?? [];

            return Result.Success(queryResults);
        }
    }
}

public class GetCheckinRawMaterialsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/checkinRawMaterials/{id}", async (Guid id, ISender sender) =>
        {
            var query = new GetCheckinRawMaterials.Query() { Id = id };

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Show list of raw materials for checkin",
            Summary = "Raw Material List",
            Tags =
            [
                new() {
                    Name = "Product"
                }
            ]
        });
    }
}
