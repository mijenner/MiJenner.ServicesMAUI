using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MiJenner.ServicesMAUI; 

namespace SettingsManagerMauiDemo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Configure AppSettings and UserSettings
            var appSettings = new AppSettings { ConnString = "database.sqlite3", ApiString = "anAPIstring", IsRunningOK = true };
            var userSettings = new UserSettings { UserName = "Bent", WantsNotifications = false };
            var settingsFileName = "settings.json";

            // Register dependencies in DI container
            builder.Services.AddSingleton<AppSettings>(appSettings);
            builder.Services.AddSingleton<UserSettings>(userSettings);
            builder.Services.AddSingleton<SettingsManagerMAUI<AppSettings, UserSettings>>(sp =>
            {
                var appSettings = sp.GetRequiredService<AppSettings>();
                var userSettings = sp.GetRequiredService<UserSettings>();
                return new SettingsManagerMAUI<AppSettings, UserSettings>(settingsFileName, appSettings, userSettings);
            });

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
