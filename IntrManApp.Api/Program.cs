using Carter;
using Dapper;
using FluentValidation;
using IntrManApp.Api;
using IntrManApp.Api.Database;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Data;
using System.Text.Json;
using IntrManApp.Shared.Common;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddWindowsService( options =>
{
    options.ServiceName = "Gha2z ERP Backend Service";
});
builder.Services.AddHostedService<BackendService>();

string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Gha2z ERP");
if (!Directory.Exists(appDataPath)) Directory.CreateDirectory(appDataPath);


appDataPath = Path.Combine(appDataPath, "Backend Service");
if(!Directory.Exists(appDataPath)) Directory.CreateDirectory(appDataPath);

var logPath = Path.Combine(appDataPath, "logs");
if (!Directory.Exists(logPath)) Directory.CreateDirectory(logPath);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File(Path.Combine(logPath, "log.txt"), rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.AddSerilog();

var appSettingFile = Path.Combine(appDataPath, "appsettings.json");
ServerAppSettings setting;


if(!File.Exists(appSettingFile))
{
    setting = new();
    Log.Logger.Information("Creating a new setting file ...");
    File.WriteAllText(appSettingFile, JsonSerializer.Serialize(setting));
}
var fileContent = File.ReadAllText(appSettingFile);
setting = JsonSerializer.Deserialize<ServerAppSettings>(fileContent) ?? new();
//if(!File.Exists(appSettingFile))
////{
////    
////    
////    //if (setting.ConnectionStrings.Database.Contains(@"\\")) setting.ConnectionStrings.Database = setting.ConnectionStrings.Database.Replace(@"\\", @"\");
////    //if (setting.ConnectionStrings.DbConfig.Contains(@"\\")) setting.ConnectionStrings.Database = setting.ConnectionStrings.DbConfig.Replace(@"\\", @"\");
////    Log.Logger.Information($"Reading app settings from {appSettingFile} =>\n{fileContent} dbCs={setting.ConnectionStrings.Database}");
////    //File.WriteAllText(appSettingFile, JsonSerializer.Serialize(setting));
////}
////else


builder.Configuration.AddJsonFile(appSettingFile,false,true);

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));

builder.Services.AddDbContext<Gha2zErpDbContext>(o =>
{
    var cs = builder.Configuration.GetConnectionString("Database");
    Log.Logger.Information($"Configuring database connection => \"{cs}\"");
    o.UseSqlServer(cs);
    //o.EnableSensitiveDataLogging();

});

builder.Services.AddScoped<IDbConnectionFactory, SqlConnectionFactory>();
builder.Services.AddScoped<IDbConfigConnectionFactory, SqlConfigConnectionFactory>();

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(setting.Port); // http
});
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(assembly));

builder.Services.AddCarter();

builder.Services.AddValidatorsFromAssembly(assembly);

var app = builder.Build();

{
    string queryString = string.Empty;
    string sps = string.Empty;
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<Gha2zErpDbContext>();

    Log.Logger.Information("Checking if database \"Gha2zERPDB\" exists");
    bool dbCreated = context.Database.CanConnect();
    bool dbJustCreated = false;

    if (!dbCreated)
    {
       
        Log.Logger.Information($"No such database found. Creating database ...");
        try
        {
            dbCreated = context.Database.EnsureCreated();
            dbJustCreated = dbCreated;
        } catch (SqlException ex)
        {
            Log.Logger.Error(ex, $"Error creating database\nException Message:{ex.Message}\n\nException ToString:{ex}");
        }
        sps = Path.Combine(appDataPath, "SPs.sql");
        Log.Logger.Information("Verifying stored procedures ...");
#if DEBUG
        Log.Logger.Information($"Copying {sps}");
        File.WriteAllText(sps, File.ReadAllText(Path.Combine("SPs.sql")));
#endif
        if (File.Exists(sps) && dbCreated)
        {
            var dbConnectionFactory = scope.ServiceProvider.GetRequiredService<IDbConnectionFactory>();
            using IDbConnection connection = dbConnectionFactory.CreateOpenConnection();
            queryString = File.ReadAllText(sps);
            var queries = queryString.Split("GO", StringSplitOptions.RemoveEmptyEntries);
            foreach (var query in queries)
            {
                try { 
                    Log.Logger.Information("Executing query: {query}", query);
                    connection.Execute(query);
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, "Error executing query: {query}", query);
                }
            }
        }
        else
        {
            if(!dbCreated) Log.Logger.Information($"Skipping database operation since the database creation was not successful");
            else Log.Logger.Information($"No stored procedures and related script found => {sps}");
        }
    }
    sps = Path.Combine(appDataPath, "ConfigSPs.sql");
    Log.Logger.Information($"Verifying config stored procedures => {sps}");

#if DEBUG
    Log.Logger.Information($"Copying {sps}");
    File.WriteAllText(sps, File.ReadAllText(Path.Combine("ConfigSPs.sql")));
#endif

    if (dbCreated && File.Exists(sps))
    {
        Log.Logger.Information($"Executing config stored procedures => {sps}");
        var dbConfigConnectionFactory = scope.ServiceProvider.GetRequiredService<IDbConfigConnectionFactory>();
        using IDbConnection connection = dbConfigConnectionFactory.CreateOpenConnection();
        queryString = File.ReadAllText(sps);
        var queries = queryString.Split("GO", StringSplitOptions.RemoveEmptyEntries);
        foreach (var query in queries)
        {
            try
            {
                Log.Logger.Information("Executing query: {query}", query);
                connection.Execute(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Error executing query: {query}", query);
            }
        }
    }
    else 
    {
        if (!dbCreated) Log.Logger.Information($"Skipping database operation since the database creation was not successful");
        else Log.Logger.Information($"No dbconfig stored procedures and related script found => {sps}");
    }

    sps = Path.Combine(appDataPath, "FeatureUpdates.sql");
    Log.Logger.Information($"Verifying feature updates => {sps}");

#if DEBUG
    Log.Logger.Information($"Copying {sps}");
    File.WriteAllText(sps, File.ReadAllText(Path.Combine("FeatureUpdates.sql")));
#endif

    if (dbCreated && File.Exists(sps))
    {
        Log.Logger.Information($"Executing feature updates => {sps}");
        var dbConnectionFactory = scope.ServiceProvider.GetRequiredService<IDbConnectionFactory>();
        using IDbConnection connectionConf = dbConnectionFactory.CreateOpenConnection();
        queryString = File.ReadAllText(sps);
        var confQueries = queryString.Split("GO", StringSplitOptions.RemoveEmptyEntries);
        foreach (var query in confQueries)
        {
            try
            {
                Log.Logger.Information("Executing query: {query}", query);
                connectionConf.Execute(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Error executing query: {query}", query);
            }
        }
    }
    else
    {
        if (!dbCreated) Log.Logger.Information($"Skipping database operation since the database creation was not successful");
        else Log.Logger.Information($"No feature updates and related script found => {sps}");
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.MapCarter();


//app.Urls.Add("http://0.0.0.0:5096");
app.Run();
