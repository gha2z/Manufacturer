using CommunityToolkit.Maui;
using InputKit.Shared.Controls;
using UraniumUI;
using UraniumUI.Icons.MaterialSymbols;

namespace IntrManAppUranium.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseUraniumUIBlurs()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                    fonts.AddMaterialSymbolsFonts();

                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddCommunityToolkitDialogs();
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IProductService, ProductService>();
            builder.Services.AddSingleton<ProductViewModel>();
            builder.Services.AddSingleton<ProductListPage>();
            return builder.Build();
        }
    }
}
