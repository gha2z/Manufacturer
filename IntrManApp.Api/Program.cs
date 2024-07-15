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
    options.ServiceName = "IntrMan Backend Service";
});
builder.Services.AddHostedService<BackendService>();

string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Intrepid Manufacture App");
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

ServerAppSettings setting;
var appSettingFile = Path.Combine(appDataPath, "appsettings.json"); 
if(File.Exists(appSettingFile))
{
    setting = JsonSerializer.Deserialize<ServerAppSettings>(File.ReadAllText(appSettingFile)) ?? new();
    if (setting.ConnectionStrings.Database.Contains(@"\\")) setting.ConnectionStrings.Database = setting.ConnectionStrings.Database.Replace(@"\\", @"\");
    if (setting.ConnectionStrings.DbConfig.Contains(@"\\")) setting.ConnectionStrings.Database = setting.ConnectionStrings.DbConfig.Replace(@"\\", @"\");
    File.WriteAllText(appSettingFile, JsonSerializer.Serialize(setting));
}
else
{
    setting = new ();
    File.WriteAllText(appSettingFile, JsonSerializer.Serialize(setting));
}

builder.Configuration.AddJsonFile(appSettingFile,false,true);

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));

builder.Services.AddDbContext<IntrManDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
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
    var context = scope.ServiceProvider.GetRequiredService<IntrManDbContext>();

    Log.Logger.Information("Checking if database \"IntrManDB\" exists");
    if(!context.Database.CanConnect())
    {
        Log.Logger.Information("No such database found. Creating database ...");
        context.Database.EnsureCreated();
        sps = Path.Combine(appDataPath, "SPs.sql");
        Log.Logger.Information("Verifying stored procedures ...");
        if (File.Exists(sps))
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
    }
    sps = Path.Combine(appDataPath, "ConfigSPs.sql");
    

    if (File.Exists(sps))
    {
        Log.Logger.Information($"Verifying config stored procedures => {sps}");
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

    sps = Path.Combine(appDataPath, "FeatureUpdates.sql");
    if (File.Exists(sps))
    {
        Log.Logger.Information($"Verifying feature updates => {sps}");
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
