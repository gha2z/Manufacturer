using Microsoft.Extensions.Logging;
using Radzen;
using Microsoft.Maui.LifecycleEvents;
using Serilog.Extensions.Logging;
using Serilog.Core;
using Serilog;
using IntrManHybridApp.UI.Services;
using Polly;
using Polly.Extensions.Http;

namespace IntrManHybridApp.UI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("MaterialIcons-Regular.woff", "MaterialIconsRegular");
            });

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddRadzenComponents();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif





#if WINDOWS
    var logPath = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.CommonApplicationData),  $"{AppInfo.Name}");
    if (!Directory.Exists(logPath)) Directory.CreateDirectory(logPath);
    logPath = Path.Combine(logPath, "logs");
    if (!Directory.Exists(logPath)) Directory.CreateDirectory(logPath);
    logPath += "\\";
    var levelSwitch = new LoggingLevelSwitch();
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.ControlledBy(levelSwitch)
        .WriteTo.Debug()
        .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
        .CreateLogger();

    builder.ConfigureLifecycleEvents(events =>
    {
        events.AddWindows(wndLifeCycleBuilder =>
        {
            wndLifeCycleBuilder.OnWindowCreated(window =>
            {
                IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                Microsoft.UI.WindowId win32WindowsId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
                Microsoft.UI.Windowing.AppWindow winuiAppWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(win32WindowsId);
                if (winuiAppWindow.Presenter is Microsoft.UI.Windowing.OverlappedPresenter p)
                {
               
                    p.Maximize();
                    //p.IsResizable = false;
                    //p.IsMaximizable = false;
                    //p.IsMinimizable = false;
                }
            });
        });
    });
#endif
        builder.Logging.AddSerilog();

        var uri = new Uri("https://localhost:7274/api/");
        var bartenderUri = new Uri("http://localhost:5159/api/");
        builder.Services.AddHttpClient<ISupplierService, SupplierService>(client => { client.BaseAddress = uri; });
        builder.Services.AddHttpClient<ILocationService, LocationService>(client => { client.BaseAddress = uri; });
        builder.Services.AddHttpClient<ICustomerService, CustomerService>(client => { client.BaseAddress = uri; });
        builder.Services.AddHttpClient<IProductService, ProductService>(client => { client.BaseAddress = uri; });
        builder.Services.AddHttpClient<ICheckinService, CheckinService>(client => { client.BaseAddress = uri; });
        builder.Services.AddHttpClient<ILabelPrintingService, LabelPrintingService>(client => { client.BaseAddress = bartenderUri; });

        //builder.Services.AddTransient<ISupplierService, SupplierService>();
        //builder.Services.AddTransient<ILocationService, LocationService>();

        return builder.Build();
    }

    static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}
