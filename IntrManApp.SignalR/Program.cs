using IntrManApp.SignalR.Components;
using Microsoft.AspNetCore.ResponseCompression;
using BlazorSignalRApp.Hubs;
using Serilog;
using IntrManApp.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
          new[] { "application/octet-stream" });
});

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

builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "IntrMan Signalr Service";
});
builder.Services.AddHostedService<SignalrService>();


builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(39500); // http
});

string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Gha2z ERP");
if (!Directory.Exists(appDataPath)) Directory.CreateDirectory(appDataPath);

var logPath = Path.Combine(appDataPath, "SignalR Service");
if (!Directory.Exists(logPath)) Directory.CreateDirectory(logPath);
logPath = Path.Combine(logPath, "logs");
if (!Directory.Exists(logPath)) Directory.CreateDirectory(logPath);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File(Path.Combine(logPath, "log.txt"), rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.AddSerilog();



var app = builder.Build();

app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseResponseCompression();
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
//app.Urls.Add("http://localhost:3108");

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapHub<ChatHub>("/chathub");

app.Run();
