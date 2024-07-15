using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using MediatR;
using System.Data;
using System.Text.Json;

namespace IntrManApp.Api.Features.Config;

public static class GetDatabaseSetting
{
    public class Command : IRequest<Result<ServerSettingResponse>>
    {
        public string Db { get; set; } = "IntrManDb";
    }

   
    internal sealed class Handler(IDbConfigConnectionFactory dbConnectionFactory) : IRequestHandler<Command, Result<ServerSettingResponse>>
    {

        private readonly IDbConfigConnectionFactory _dbConnectionFactory = dbConnectionFactory;
        public async Task<Result<ServerSettingResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Intrepid Manufacture App");
                if (!Directory.Exists(appDataPath)) Directory.CreateDirectory(appDataPath);

                appDataPath = Path.Combine(appDataPath, "Backend Service");
                if (!Directory.Exists(appDataPath)) Directory.CreateDirectory(appDataPath);

                appDataPath = Path.Combine(appDataPath, "appSettings.json");

                var appSettings = JsonSerializer.Deserialize<ServerAppSettings>(File.ReadAllText(appDataPath)) ?? new();
                var dbConnectionStrings = appSettings.ConnectionStrings.Database.Split(';');
                var serverSetting = new ServerSettingResponse
                {
                    DatabaseServer = dbConnectionStrings[0].Split('=')[1],
                    UserId = dbConnectionStrings[2].Split('=')[1],
                    Password = dbConnectionStrings[3].Split('=')[1],
                    UseIntegratedSecurity = dbConnectionStrings[5].Split('=')[1].ToLower() == "true",
                    Port = appSettings.Port
                };
                using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();
                var result = await connection.QueryFirstAsync<SetBackupDiskPathRequest>
                    ($"select '{request.Db}' as Db, " +
                     $"isnull((select Value from dbo.Config where Name='BackupFolder' and Context='{request.Db}'),'') as Path, " +
                     $"isnull((select Value from dbo.Config where Name='BackupFileName' and Context='{request.Db}'),'') as FileName," +
                     $"isnull(cast((select Value from dbo.Config where Name='BackupAppendDateToFileName' and Context='{request.Db}') as bit)," +
                     $"cast(1 as bit)) as AppendDateTime");
                serverSetting.BackupPath = result.Path;
                serverSetting.BackupFileName = result.FileName;
                serverSetting.AppendDateTime = result.AppendDateTime;

                return Result.Success(serverSetting);
            }
            catch (Exception ex)
            {
                return Result.Failure<ServerSettingResponse>(new Error("GetDatabaseSetting.Error", ex.Message));
            }
        }
    }
}

public class GetDatabaseSettingsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/GetDatabaseSetting", async (ISender sender) =>
        {
            var command = new GetDatabaseSetting.Command();

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
