namespace MiJenner.ServicesMAUI
{
    public interface ISettingsService
    {
        // Retrieves a setting of type T associated with the provided key.
        // Parameters:
        // - key: The unique identifier used to retrieve the setting value.
        // - defaultValue: The value to return if no setting is found for the key.
        // Returns a Task containing the value of type T, or the defaultValue if the key is not found.
        Task<T> Get<T>(string key, T defaultValue);

        // Saves a setting of type T with the provided key and value.
        // Parameters:
        // - key: The unique identifier used to store the setting.
        // - value: The data of type T to be saved.
        // Returns a Task to allow for asynchronous operation.
        Task Save<T>(string key, T value);
    }
}
