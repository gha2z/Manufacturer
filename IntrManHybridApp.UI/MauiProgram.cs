using Microsoft.Extensions.Logging;
using Radzen;
using Microsoft.Maui.LifecycleEvents;
using Serilog.Extensions.Logging;
using Serilog.Core;
using Serilog;
using IntrManHybridApp.UI.Services;
using IntrManApp.Shared.Common;
using System.Text.Json;

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

        string appDataPath = string.Empty;

#if WINDOWS
        appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), AppInfo.Name);
#endif
#if ANDROID
        appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ".local/share");
#endif



        if (!Directory.Exists(appDataPath)) Directory.CreateDirectory(appDataPath);

        appDataPath = Path.Combine(appDataPath, "Client App");
        if (!Directory.Exists(appDataPath)) Directory.CreateDirectory(appDataPath);

        var logPath = Path.Combine(appDataPath, "logs");
        if (!Directory.Exists(logPath)) Directory.CreateDirectory(logPath);
        logPath += "\\";
        var levelSwitch = new LoggingLevelSwitch();
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.ControlledBy(levelSwitch)
            .WriteTo.Debug()
            .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
            .CreateLogger();


        builder.Logging.AddSerilog();

        var appSettingJsonFile = Path.Combine(appDataPath, "AppSettings.json");

        var settings = new ClientAppSettingLoader();

        if (!File.Exists(appSettingJsonFile))
        {
            settings.ApiBaseUrl = "localhost";
            settings.ApiBasePort = 50001;
            settings.AppDataPath = appDataPath;
            settings.ApiUrlVerb = "http";
            File.WriteAllText(appSettingJsonFile, JsonSerializer.Serialize(settings));
        }
        if (File.Exists(appSettingJsonFile))
        {
            //builder.Configuration.Sources.Add(new JsonConfigurationSource { Path = appSettingJsonFile, Optional = false, ReloadOnChange = true });
            settings = new ClientAppSettingLoader();
            settings = JsonSerializer.Deserialize<ClientAppSettingLoader>(File.ReadAllText(appSettingJsonFile));
            AppSettings.ApiBaseUrl = settings?.ApiBaseUrl ?? "localhost";
            AppSettings.ApiBasePort = settings?.ApiBasePort ?? 50001;
            AppSettings.AppDataPath = settings?.AppDataPath ?? string.Empty;
            AppSettings.ApiUrlVerb = settings?.ApiUrlVerb ?? "http";
        }

        //builder.Services.AddSingleton<IFolderPicker>(FolderPicker.Default);
        //builder.Services.AddTransient<PickFolder, FolderPickerViewModel>();

        var uri = new Uri($"{AppSettings.ApiUrlVerb}://{AppSettings.ApiBaseUrl}:{AppSettings.ApiBasePort}/api/");

        var bartenderUri = new Uri("http://localhost:5159/api/");
        builder.Services.AddHttpClient<ISupplierService, SupplierService>(client => { client.BaseAddress = uri; })
            .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });
        builder.Services.AddHttpClient<ILocationService, LocationService>(client => { client.BaseAddress = uri; })
            .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });
        builder.Services.AddHttpClient<ICustomerService, CustomerService>(client => { client.BaseAddress = uri; })
             .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
             {
                 ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
             });
        builder.Services.AddHttpClient<IProductService, ProductService>(client => { client.BaseAddress = uri; })
             .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
             {
                 ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
             });
        builder.Services.AddHttpClient<ICheckinService, CheckinService>(client => { client.BaseAddress = uri; })
             .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
             {
                 ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
             });
        builder.Services.AddHttpClient<IProductionService, ProductionService>(client => { client.BaseAddress = uri; })
             .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
             {
                 ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
             });
        builder.Services.AddHttpClient<ILabelPrintingService, LabelPrintingService>(client => { client.BaseAddress = bartenderUri; });
        builder.Services.AddHttpClient<ISaleService, SaleService>(client => { client.BaseAddress = uri; })
             .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
             {
                 ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
             });
        builder.Services.AddHttpClient<IInventoryService, InventoryService>(client => { client.BaseAddress = uri; })
             .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
             {
                 ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
             });
        builder.Services.AddHttpClient<IAuthService, AuthService>(client => { client.BaseAddress = uri; })
             .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
             {
                 ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
             });
        builder.Services.AddHttpClient<IAppCustomConfig, AppCustomConfig>(client => { client.BaseAddress = uri; })
             .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
             {
                 ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
             });


#if WINDOWS
   
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

        return builder.Build();
    }
}
