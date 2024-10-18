using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Text.Json;
using MiJenner.ServicesMaui;
using System.Diagnostics;

/// <summary>
/// AppSettingsService strives to make it easier to store application settings
/// across Console, Maui and Web-API projects. 
/// 
/// AppSettingsServiceMaui has the following policy: 
/// - Use JSON file as configuration-provider. 
/// - Receive AppName and CompanyName. 
/// - See if appsettings.json file is present already. If it is, use it. 
/// - If not present, attempt to copy from packaged appsettings.json 
/// - If not create one based on user supplied info. 
/// 
/// Finally: Offer IConfiguration object for handling configurations using JSON. 
/// 
/// Usage: 
/// AppSettingsServiceMaui appSettingsService = AppSettingsServiceMaui("Company Name", "App Name", "appsettings.json")
/// 
/// or 
/// 
/// AppSettingsServiceMaui appSettingsService = AppSettingsService("Company Name", "App Name", "")
/// 
/// Then 
/// 
/// IConfiguration config = appSettingsService.GetConfiguration(); 
/// 
/// </summary>
public class AppSettingsServiceMaui : IAppSettingsService
{
    // IConfiguration object: 
    private IConfiguration configuration;
    // Bool to tell user if configuration file exists, if not use CreateSettingsFile. 
    private bool configExists = false;
    public bool ConfigExists { get { return configExists; } }

    #region 
    // Configuration filename: 
    public string fileName = "appsettings.json";
    // Full folder path to but excluding "appsettings.json": 
    private string folderPath;
    public string FolderPath { get => folderPath; }
    // Full path to "appsettings.json" including filename: 
    private string filePath;
    public string FilePath { get => filePath; }

    // appname and companyname and folder policy 
    private string companyName = "companyname";
    private string appName = "appname";

    #endregion

    public AppSettingsServiceMaui(string? companyName, string? appName, string? fileName = "appsettings.json")
    {
        // Set filename and ... 
        if (!String.IsNullOrEmpty(fileName)) { this.fileName = fileName; } else { this.fileName = "appsettings.json"; };
        if (!String.IsNullOrEmpty(appName)) { this.appName = appName; };
        if (!String.IsNullOrEmpty(companyName)) { this.companyName = companyName; };
        SetFileAndFolderPaths();

        ConditionalCopyPackagedSettingsFile();

        // Build a configuration object: 
        if (String.IsNullOrEmpty(this.fileName))
        {
            Debug.WriteLine("fileName not correctly set");
            return;
        }
        if (String.IsNullOrEmpty(FolderPath))
        {
            Debug.WriteLine("FolderPath not correctly set");
        }

        RefreshConfiguration();
    }

    public void RefreshConfiguration()
    {
        if (!File.Exists(filePath))
        {
            return;
        } // todo 

        var builder = new ConfigurationBuilder()
            .SetBasePath(FolderPath)
            .AddJsonFile(this.fileName, optional: false, reloadOnChange: true);

        configuration = builder.Build();
    }

    public IConfiguration GetConfiguration()
    {
        return configuration;
    }

    public string GetFilePath()
    {
        return FilePath;
    }

    public string GetFolderPath()
    {
        return FolderPath;
    }

    public void CreateSettingsFile(object aSettingsObject)
    {
        if (aSettingsObject == null)
        {
            aSettingsObject = new { ASetting = "value" }; // creates anonymous object example setting. 
        }

        // Serialize the default settings to JSON
        string json = JsonSerializer.Serialize(aSettingsObject, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json, System.Text.Encoding.UTF8);
        RefreshConfiguration();
    }

    private void SetFileAndFolderPaths()
    {
        folderPath = FileSystem.AppDataDirectory;
        filePath = Path.Combine(folderPath, fileName);
    }

    public async Task ConditionalCopyPackagedSettingsFile()
    {
        // Check if the appsettings.json file exists in the target location
        if (File.Exists(filePath))
        {
            configExists = true;
            Debug.WriteLine($"{filePath} exists and will be used");
            return;
        }
        // Check if packaged appsettings.json file exist, and use it if it exists: 
        if (await FileSystem.AppPackageFileExistsAsync(fileName))
        {
            try
            {
                using var sourceStream = await FileSystem.OpenAppPackageFileAsync(fileName);
                using var destinationStream = new FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.None);
                await sourceStream.CopyToAsync(destinationStream);
                configExists = true;
                Debug.WriteLine($"{FilePath} created from package");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"*** {ex.Message}");
                throw;
            }
            return; 
        }
        // If not we will create our own 
        Debug.WriteLine($"No {fileName} found, neither in app directory nor package");
        configExists = false;
    }

    public static void WriteConfiguration(IConfiguration config, string parentKey = "")
    {
        Debug.WriteLine("*** Write out configuration ***");
        foreach (var section in config.GetChildren())
        {
            string key = parentKey == "" ? section.Key : $"{parentKey}:{section.Key}";

            if (section.GetChildren().Any())
            {
                WriteConfiguration(section, key);
            }
            else
            {
                Debug.WriteLine($"{key}: {section.Value}");
            }
        }
    }
}
