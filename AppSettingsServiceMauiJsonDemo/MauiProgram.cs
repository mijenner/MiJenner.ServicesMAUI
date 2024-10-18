using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MiJenner.ServicesMaui;

namespace AppSettingsServiceMauiJsonDemo
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            var appSettingsService = new AppSettingsServiceMaui("companyname", "appname", "");
            builder.Services.AddSingleton<IAppSettingsService> (appSettingsService);
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>(); 

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
