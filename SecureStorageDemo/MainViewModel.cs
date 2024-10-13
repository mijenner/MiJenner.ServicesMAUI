using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MiJenner.ServicesMAUI;

namespace SecureStorageDemo
{
    public partial class MainViewModel : ObservableObject
    {
        private string accessTokenKey = "AccessToken";

        [ObservableProperty]
        private string accessToken;
        [ObservableProperty]
        private string savedAccessToken;

        private ISecureStorageService secureStorageService;

        public MainViewModel(ISecureStorageService secureStorageService)
        {
            this.secureStorageService = secureStorageService;
        }

        [RelayCommand]
        private async Task SaveTokenAsync()
        {
            await secureStorageService.Save(accessTokenKey, AccessToken);
            AccessToken = String.Empty; 
        }

        [RelayCommand]
        private async Task ShowTokenAsync()
        {
            SavedAccessToken = await secureStorageService.Get(accessTokenKey);
        }
    }
}
