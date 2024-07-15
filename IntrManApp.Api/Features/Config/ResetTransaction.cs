using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.BasicModules;

public static class ResetTransaction
{
    public class Command : IRequest<Result<bool>>
    {
    }

    internal sealed class Handler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<Command, Result<bool>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

                await connection.ExecuteAsync(
                        "dbo.ResetTransactions", commandType: CommandType.StoredProcedure);

                return Result.Success(true);
            }
            catch (Exception ex)
            {
                return Result.Failure<bool>(new Error("ResetTransaction.Error", ex.Message));
            }
        }
    }
}

public class ResetTransactionsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/ResetTransaction", async (ISender sender) =>
        {
            var command = new ResetTransaction.Command();

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Clear all saved transactions",
            Summary = "Reset Transactions",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag>
            {
                new Microsoft.OpenApi.Models.OpenApiTag
                {
                    Name = "Config"
                }
            }
        });
    }
}
