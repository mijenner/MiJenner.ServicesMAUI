namespace MiJenner.ServicesMAUI
{
    public interface ISecureStorageService
    {
        // Saves a value securely in storage associated with a specific key.
        // Parameters:
        // - key: The unique identifier used to store and retrieve the value.
        // - value: The string data to be securely stored.
        // Returns a Task to allow for asynchronous operation.
        Task Save(string key, string value);

        // Retrieves a securely stored value based on the provided key.
        // Parameters:
        // - key: The unique identifier used to retrieve the corresponding value.
        // Returns a Task containing the string value associated with the key, or null if not found.
        Task<string> Get(string key);
    }
}
