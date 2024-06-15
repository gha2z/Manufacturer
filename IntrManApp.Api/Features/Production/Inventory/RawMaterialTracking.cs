using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.Production;

public static class RawMaterialTracking
{
    public class Query : IRequest<Result<IEnumerable<RawMaterialTrackingResponse>>>
    {
        public Guid RawMaterialId { get; set; } = Guid.Empty;
        public string CartonId { get; set; } = string.Empty;
        public Guid SupplierId { get; set; } = Guid.Empty;
        public DateTime CheckInDate { get; set; } = new DateTime(2023, 12, 31);
        public DateTime CheckOutDate { get; set; } = new DateTime(2023, 12, 31);
        public DateTime ReturnDate { get; set; } = new DateTime(2023, 12, 31);
        public Guid EndProductId { get; set; } = Guid.Empty;
        public string EndProductBatchNumber { get; set; } = string.Empty;
        public DateTime EndProductionStartDate { get; set; } = new DateTime(2023, 12, 31);
    }

    internal sealed class Handler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<Query, Result<IEnumerable<RawMaterialTrackingResponse>>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<Result<IEnumerable<RawMaterialTrackingResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            IEnumerable<RawMaterialTrackingResponse>? queryResults =
                (IEnumerable<RawMaterialTrackingResponse>?)await connection.QueryAsync<RawMaterialTrackingResponse>(
                    "Production.RawMaterialTracking", new { 
                        request.RawMaterialId, 
                        request.CartonId,
                        request.SupplierId,
                        request.CheckInDate,
                        request.CheckOutDate,
                        request.ReturnDate,
                        request.EndProductId,
                        request.EndProductBatchNumber,
                        request.EndProductionStartDate 
                    }, commandType: CommandType.StoredProcedure);

            if (queryResults == null)
            {
                return Result.Failure<IEnumerable<RawMaterialTrackingResponse>>(new Error("RawMaterialTracking.NotFound", "No products found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class RawMaterialTrackingEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/rawMaterialTracking", async (RawMaterialTrackingRequest request, ISender sender) =>
        {
            var query = request.Adapt<RawMaterialTracking.Query>();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Tracking Raw Material based on request",
            Summary = "Raw Material Tracking",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag>
            {
                new Microsoft.OpenApi.Models.OpenApiTag
                {
                    Name = "Raw Material"
                }
            }
        });
    }
}
