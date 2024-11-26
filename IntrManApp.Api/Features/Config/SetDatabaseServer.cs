using Carter;
using Dapper;
using IntrManApp.Api.Database;
using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Data;
using System.Text.Json;

namespace IntrManApp.Api.Features.Config;

public static class SetDatabaseServer
{
    public class Command : IRequest<Result<bool>>
    {
        public string Server { get; set; } = "localhost";
        public string UserId { get; set; } = "";
        public string Password { get; set; } = "";
        public bool UseIntegratedSecurity { get; set; } = true;
        public int Port { get; set; } = 39501;
    }


    internal sealed class Handler(IDbConfigConnectionFactory dbConfigConnectionFactory,
        IDbConnectionFactory dbConnectionFactory,
        Gha2zErpDbContext context) : IRequestHandler<Command, Result<bool>>
    {
        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Gha2z ERP");
                if (!Directory.Exists(appDataPath)) Directory.CreateDirectory(appDataPath);

                appDataPath = Path.Combine(appDataPath, "Backend Service");
                if (!Directory.Exists(appDataPath)) Directory.CreateDirectory(appDataPath);

                appDataPath = Path.Combine(appDataPath, "appSettings.json");

                var appSettings = JsonSerializer.Deserialize<ServerAppSettings>(File.ReadAllText(appDataPath)) ?? new();
                appSettings.Port = request.Port;
                appSettings.ConnectionStrings.Database=
                    $"Server={request.Server};Database=Gha2zERPDB;User Id={request.UserId};Password={request.Password};" +
                    $"TrustServerCertificate=True;Integrated Security={request.UseIntegratedSecurity}";
                appSettings.ConnectionStrings.DbConfig =
                  $"Server={request.Server};Database=Master;User Id={request.UserId};Password={request.Password};" +
                  $"TrustServerCertificate=True;Integrated Security={request.UseIntegratedSecurity}";
               
                File.WriteAllText(appDataPath, JsonSerializer.Serialize<ServerAppSettings>(appSettings));

                dbConfigConnectionFactory.SetConnectionString(appSettings.ConnectionStrings.DbConfig);
                dbConnectionFactory.SetConnectionString(appSettings.ConnectionStrings.Database);
                context.Database.SetConnectionString(appSettings.ConnectionStrings.Database);

                string sps = Path.Combine(AppContext.BaseDirectory, "SPs.sql");
                if (!context.Database.CanConnect())
                {
                    Log.Logger.Information("No such database found. Creating database ...");
                    context.Database.EnsureCreated();
                    Log.Logger.Information("Verifying stored procedures ...");
                    if (File.Exists(sps))
                    {

                        var queryString = File.ReadAllText(sps);
                        var queries = queryString.Split("GO", StringSplitOptions.RemoveEmptyEntries);
                        using IDbConnection connection = await dbConnectionFactory.CreateOpenConnectionAsync();
                        foreach (var query in queries)
                        {
                            connection.Execute(query);
                        }
                    }
                }
             
               
                sps = Path.Combine(AppContext.BaseDirectory, "ConfigSPs.sql");


                if (File.Exists(sps))
                {
                    using IDbConnection connection = await dbConfigConnectionFactory.CreateOpenConnectionAsync();
                    var queryString = File.ReadAllText(sps);
                    var queries = queryString.Split("GO", StringSplitOptions.RemoveEmptyEntries);
                    foreach (var query in queries)
                    {
                        connection.Execute(query);
                    }
                }

                sps = Path.Combine(AppContext.BaseDirectory, "FeatureUpdates.sql");
                if (File.Exists(sps))
                {
                    using IDbConnection connection = dbConnectionFactory.CreateOpenConnection();
                    var queryString = File.ReadAllText(sps);
                    var queries = queryString.Split("GO", StringSplitOptions.RemoveEmptyEntries);
                    foreach (var query in queries)
                    {
                        connection.Execute(query);
                    }
                }
                return Result.Success(true);
            }
            catch (Exception ex)
            {
                return Result.Failure<bool>(new Error("SetDatabaseServer.Error", ex.Message));
            }
        }
    }
}

public class SetDatabaseServersEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/SetDatabaseServer", async (SetDatabaseServerRequest request, ISender sender) =>
        {
            var command = request.Adapt<SetDatabaseServer.Command>();

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
