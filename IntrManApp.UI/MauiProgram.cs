using IntrManHyridApp.UI.Services;
using IntrManHyridApp.UI.View.Products;
using Microsoft.Extensions.Logging;
using UraniumUI;

namespace IntrManHyridApp.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddMaterialIconFonts();
                });
                

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<ProductService>();
            builder.Services.AddSingleton<ProductViewModel>();
            builder.Services.AddSingleton<NewPage1>();
            builder.Services.AddSingleton<NewPage2>();

            return builder.Build();
        }
    }
}
