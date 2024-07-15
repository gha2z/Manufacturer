using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Config;

public static class BackupDatabase
{
    public class Command : IRequest<Result<BackupRestoreDbResult>>
    {
        public string DB { get; set; } = "IntrManDb";
    }

    internal sealed class Handler(IDbConfigConnectionFactory dbConnectionFactory) : IRequestHandler<Command, Result<BackupRestoreDbResult>>
    {
        private readonly IDbConfigConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<Result<BackupRestoreDbResult>> Handle(Command request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            BackupRestoreDbResult? queryResults =
                (BackupRestoreDbResult?)await connection.QueryFirstAsync<BackupRestoreDbResult>(
                    "dbo.BackupDB", new { request.DB }, commandType: CommandType.StoredProcedure );

            if (queryResults == null)
            {
                return Result.Failure<BackupRestoreDbResult>(new Error("BackupDatabase.NotFound", "No products found"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class BackupDatabaseEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/BackupDatabase", async (ISender sender) =>
        {
            var command = new BackupDatabase.Command();

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Backup IntrManDb database into the specified path on disk",
            Summary = "Backup Database",
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
