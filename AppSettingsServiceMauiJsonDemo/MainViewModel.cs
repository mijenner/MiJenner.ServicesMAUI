using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Configuration;
using MiJenner.ServicesMaui;
using System.Diagnostics;

namespace AppSettingsServiceMauiJsonDemo
{
    public class MainViewModel : ObservableObject 
    {
        IAppSettingsService appSettingsService; 

        public MainViewModel(IAppSettingsService _appSettingsService)
        {
            Debug.WriteLine($"App data folder is {FileSystem.AppDataDirectory}"); 

            this.appSettingsService = _appSettingsService;
            if (!appSettingsService.ConfigExists)
            {
                appSettingsService.CreateSettingsFile(new { CompanyName = "CompanyName3", AppName="AppName3", Version="0.123" });
            }
            IConfiguration configuration = appSettingsService.GetConfiguration();
            Debug.WriteLine(configuration["CompanyName"] ?? "Company name not in config file");
            Debug.WriteLine(configuration["AppName"] ?? "AppName not in config file");
            Debug.WriteLine(configuration["Version"] ?? "Version not in config file");

            AppSettingsServiceMaui.WriteConfiguration(configuration);
        }
    }
}
