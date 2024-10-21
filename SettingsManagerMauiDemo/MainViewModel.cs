using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MiJenner.ServicesMAUI;

namespace SettingsManagerMauiDemo
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        string connString;
        [ObservableProperty]
        bool wantsNotifications; 

        AppSettings appSettings;
        UserSettings userSettings;
        [ObservableProperty]
        SettingsManagerMAUI<AppSettings, UserSettings> settings; 

        public MainViewModel(SettingsManagerMAUI<AppSettings, UserSettings> settingsManager, AppSettings appSettings, UserSettings userSettings)
        {
            this.settings = settingsManager;
            this.appSettings = appSettings;
            this.userSettings = userSettings;
        }

        [RelayCommand]
        async Task Initialize()
        {
            await settings.Initialize();
            ConnString = appSettings.ConnString;
            WantsNotifications = userSettings.WantsNotifications;
        }

        [RelayCommand]
        async Task SaveSettings()
        {
            appSettings.ConnString = ConnString;
            userSettings.WantsNotifications = WantsNotifications;

            await settings.SaveSettingsToDiskAsync();
            await Shell.Current.DisplayAlert("Saved!", "Settings are saved", "OK");
        }
    }
}
