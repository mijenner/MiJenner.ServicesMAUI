﻿namespace MiJenner.ServicesMAUI
{
    public class DataStorageBin : IDataStorageBin
    {
        private readonly string _directoryPath;

        public DataStorageBin()
        {
            _directoryPath = FileSystem.Current.AppDataDirectory;
        }

        public async Task<bool> SaveBinary(string fileName, byte[] data)
        {
            try
            {
                var fullPath = Path.Combine(_directoryPath, fileName);
                await File.WriteAllBytesAsync(fullPath, data);
                return true;
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., logging)
                Console.WriteLine($"Error saving binary data: {ex.Message}");
                return false;
            }
        }

        public async Task<byte[]> ReadBinary(string fileName)
        {
            try
            {
                var fullPath = Path.Combine(_directoryPath, fileName);
                return await File.ReadAllBytesAsync(fullPath);
            }
            catch (FileNotFoundException)
            {
                // Handle the case when the file does not exist
                Console.WriteLine($"File not found: {fileName}");
                return Array.Empty<byte>(); // Return an empty array if the file doesn't exist
            }
            catch (Exception ex)
            {
                // Handle other exceptions (e.g., logging)
                Console.WriteLine($"Error reading binary data: {ex.Message}");
                return Array.Empty<byte>(); // Return an empty array in case of error
            }
        }

        public async Task<bool> DeleteBinary(string fileName)
        {
            try
            {
                var fullPath = Path.Combine(_directoryPath, fileName);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true; 
                }
                return false; 
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., logging)
                Console.WriteLine($"Error deleting binary file: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> FileExists(string fileName)
        {
            // Check if the file exists in the storage
            return File.Exists(Path.Combine(_directoryPath, fileName));
        }

        public async Task<IEnumerable<string>> GetAllFiles()
        {
            // Get a list of all binary files in the storage directory
            return Directory.GetFiles(_directoryPath, "*.bin").Select(Path.GetFileName);
        }
    }

}
