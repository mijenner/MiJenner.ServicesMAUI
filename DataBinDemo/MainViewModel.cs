using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MiJenner.ServicesMAUI;

namespace DataBinDemo
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ImageSource imageSource;
        [ObservableProperty]
        private ImageSource storedImageSource;

        private byte[] imageBytes;
        private string imageFilePath = "monkeyImage.bin";
        private string imageUrl = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/baboon.jpg";
        // private string imageUrl = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/capuchin.jpg";

        private readonly IDataStorageBin dataStorageBin;

        public MainViewModel(IDataStorageBin dataStorageBin)
        {
            this.dataStorageBin = dataStorageBin;
        }

        [RelayCommand]
        public async Task FetchImageAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                imageBytes = await httpClient.GetByteArrayAsync(imageUrl);
                ImageSource = ImageSource.FromStream(() => new MemoryStream(imageBytes));
            }
        }

        [RelayCommand]
        public async Task SaveImageAsync()
        {
            if (ImageSource is null) return;

            await dataStorageBin.SaveBinary(imageFilePath, imageBytes);
            await Shell.Current.DisplayAlert("Image Saved", "Monkey image has been saved locally!", "OK");
        }

        [RelayCommand]
        public async Task LoadImageAsync()
        {
            imageBytes = await dataStorageBin.ReadBinary(imageFilePath);
            if (imageBytes != null)
            {
                StoredImageSource = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                await Shell.Current.DisplayAlert("Image Loaded", "Monkey image has been loaded!", "OK");
            }
        }
    }
}
