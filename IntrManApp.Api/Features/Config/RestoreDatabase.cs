using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Features.BasicModules;

public static class RestoreDatabase
{
    public class Command : IRequest<Result<BackupRestoreDbResult>>
    {
        public string DB { get; set; } = "Gha2zERPDB";
        public string Path { get; set; } = string.Empty;
    }

    internal sealed class Handler(IDbConfigConnectionFactory dbConnectionFactory) : IRequestHandler<Command, Result<BackupRestoreDbResult>>
    {
        private readonly IDbConfigConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<Result<BackupRestoreDbResult>> Handle(Command request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            BackupRestoreDbResult? queryResults =
                (BackupRestoreDbResult?)await connection.QueryFirstAsync<BackupRestoreDbResult>(
                    "dbo.RestoreDB", new { request.DB, request.Path }, commandType: CommandType.StoredProcedure);

            if (queryResults == null)
            {
                return Result.Failure<BackupRestoreDbResult>(new Error("RestoreDatabase.NotFound", "Db restoration failed"));
            }
            else
            {
                return Result.Success(queryResults);
            }
        }
    }
}

public class RestoreDatabaseEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/RestoreDatabase", async (RestoreDatabaseRequest request, ISender sender) =>
        {
            var command = request.Adapt<RestoreDatabase.Command>();

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Description = "Restore Gha2zERPDB database from a specified backup file",
            Summary = "Restore Database",
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
