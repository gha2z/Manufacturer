using Microsoft.Extensions.Logging;
using Radzen;
using Microsoft.Maui.LifecycleEvents;

namespace IntrManHybridApp.UI
{
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

            builder.Services.AddHttpClient("BackendAPI", client =>
                client.BaseAddress = new Uri("https://localhost:7274/api/"));
            return builder.Build();
        }
    }
}
