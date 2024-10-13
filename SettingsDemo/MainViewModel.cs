using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MiJenner.ServicesMAUI;

namespace SettingsDemo
{
    public partial class MainViewModel : ObservableObject 
    {
        private ISettingsService _settingsService;

        public MainViewModel(ISettingsService settingsService)
        {
            this._settingsService = settingsService;
            LoadData();
        }

        [ObservableProperty]
        private bool sendNotifications;

        public async Task LoadData()
        {
            SendNotifications = await _settingsService.Get<bool>(nameof(SendNotifications), false);
        }

        [RelayCommand]
        private async Task SaveSettingsAsync()
        {
            await _settingsService.Save(nameof(SendNotifications), SendNotifications);
            await Shell.Current.DisplayAlert("Saved!", "Settings have been saved!", "OK");
        }
    }
}
