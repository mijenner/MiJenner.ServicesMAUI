### MiJenner.ServicesMAUI 
Offers a settingsmanager, a simple crud service and a few other helpers. 

## Settings manager 
To use the settings manager you create two objects, (a) one with properties representing settings for application and (b) one with properties representing settings for users. 

First, remember to add NuGet package and add: 
```cs 
using MiJenner.ServicesMAUI; 
```

You may want to use by example the following settings objects: 
```cs 
AppSettings appSettings = new AppSettings() { ConnString = "database.sqlite3", ApiString="anAPIstring", IsRunningOK = true };
UserSettings userSettings = new UserSettings() { UserName="Bent", WantsNotifications=false };
``` 
Create these in separate files. 

You typically want to inject these and the settings manager into a view model, like: 
```cs 
public MainViewModel(SettingsManagerMAUI<AppSettings, UserSettings> settingsManager, AppSettings appSettings, UserSettings userSettings)
{
   this.settings = settingsManager;
   this.appSettings = appSettings;
   this.userSettings = userSettings;
}
```
And you typically want to declare the settings, appSettings and userSettings as local instances above the constructor:  
```cs 
AppSettings appSettings;
UserSettings userSettings;
[ObservableProperty]
SettingsManagerMAUI<AppSettings, UserSettings> settings; 
```

Now we want to register these with the DI container. This is a little cumbersome due to the many injections, but could be done like: 
```cs 
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
```

In the view model I pull out the properties of the settings that I want to work with in the UI, by example: 
```cs 
[ObservableProperty]
string connString;
[ObservableProperty]
bool wantsNotifications; 
```

Now we can define command-code in the view model, by example: 
```cs 
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
```
Note: I'm using the CommunityToolkit.Mvvm to reduce amount of boilerplate code. 

Now we can use the commands via binding (assumes Page's BindingContext is set to view model): 
```xml
    xmlns:local="clr-namespace:SettingsManagerMauiDemo"
    xmlns:toolkit="http://schemas.microsoft.com/dotnet/2022/maui/toolkit"
    x:DataType="local:MainViewModel">

    <StackLayout
        Padding="30,0"
        Orientation="Vertical"
        Spacing="10">
        <Label Text="Settings Demo" />
        <Label Text="{Binding Settings.ManagerSettings.FilePath, StringFormat='Settings saved in {0}'}" />
        <StackLayout Orientation="Horizontal" Spacing="5">
            <Label FontSize="Body" Text="ConnString = " />
            <Entry Text="{Binding ConnString}" />
        </StackLayout>

        <StackLayout Orientation="Horizontal" Spacing="5">
            <Label FontSize="Body" Text="WantsNotifications = " />
            <Switch IsToggled="{Binding WantsNotifications}" />
        </StackLayout>

        <Button
            Command="{Binding InitializeCommand}"
            HorizontalOptions="Fill"
            Text="Load settings" />

        <Button
            Command="{Binding SaveSettingsCommand}"
            HorizontalOptions="Fill"
            Text="Save settings" />

    </StackLayout>
```
Note: SettingsManager.Initialize() must be called to read or create settings file. 

Now you can read and modify settings using normal "dot-notation", by example: 

```cs 
userSettings.UserName = "John Doe";
appSettings.ApiString = "Application Programming Interface"; 
``` 

You may want to save to devices disk, so the settings are available next time you run the application: 
```cs 
await settings.SaveSettingsToDiskAsync();
``` 
