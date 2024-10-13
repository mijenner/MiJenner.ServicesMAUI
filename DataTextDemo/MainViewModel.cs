using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MiJenner.ServicesMAUI;

namespace DataTextDemo
{
    public partial class MainViewModel : ObservableObject 
    {
        [ObservableProperty]
        private string text;
        [ObservableProperty]
        private string savedText; 

        IDataStorageText dataStorageText;
        private string filePath = "myAppData.txt";

        public MainViewModel(IDataStorageText dataStorageText)
        {
            this.dataStorageText = dataStorageText;
        }

        [RelayCommand]
        public async Task SaveNoteAsync()
        {
            await dataStorageText.SaveText(filePath, Text);
            Text = string.Empty; 
            await Shell.Current.DisplayAlert("Saved!", "Note has been saved!", "OK!"); 
        }

        [RelayCommand]
        public async Task ReadNoteAsync()
        {
            SavedText=await dataStorageText.ReadText(filePath); 
        }
    }
}
