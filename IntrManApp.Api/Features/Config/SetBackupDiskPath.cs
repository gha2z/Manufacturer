using Azure.Core;
using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using MediatR;
using System.Data;

namespace IntrManApp.Api.Config;

public static class SetBackupDiskPath
{
    public class Command : IRequest<Result<bool>>
    {
        public string Db { get; set; } = "IntrManDb";
        public string Path { get; set; } = string.Empty;
        public string FileName { get; set; } = "IntrManDbBackup";
        public bool AppendDateTime { get; set; } = true;
    }

    internal sealed class Handler(IDbConfigConnectionFactory dbConnectionFactory) : IRequestHandler<Command, Result<bool>>
    {
        private readonly IDbConfigConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

            var queryResult = 
                await connection.ExecuteAsync("dbo.SetBackupDiskPath", 
                    new { request.Db, request.Path, request.FileName, request.AppendDateTime }, 
                    commandType: CommandType.StoredProcedure);

            return Result.Success(queryResult>0);
            
        }
    }
}

public class SetBackupDiskPathEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/SetBackupDiskPath", async (SetBackupDiskPathRequest request, ISender sender) =>
        {
            var command = request.Adapt<SetBackupDiskPath.Command>();
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
