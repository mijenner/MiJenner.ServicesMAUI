namespace MiJenner.ServicesMAUI
{
    public class DataStorageText : IDataStorageText
    {
        private readonly string _directoryPath;

        public DataStorageText()
        {
            _directoryPath = FileSystem.Current.AppDataDirectory;
        }

        public async Task<bool> SaveText(string fileName, string text)
        {
            try
            {
                var fullPath = Path.Combine(_directoryPath, fileName);
                File.WriteAllText(fullPath, text);
                return true;
            }
            catch
            {
                // Handle exceptions (e.g., logging)
                return false;
            }
        }

        public async Task<string> ReadText(string fileName)
        {
            try
            {
                var fullPath = Path.Combine(_directoryPath, fileName);
                return File.ReadAllText(fullPath);
            }
            catch
            {
                // Handle exceptions (e.g., logging)
                return string.Empty;
            }
        }

        public async Task<bool> DeleteText(string fileName)
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
            catch
            {
                // Handle exceptions (e.g., logging)
                return false;
            }
        }

        public async Task<bool> FileExists(string fileName)
        {
            var res = File.Exists(Path.Combine(_directoryPath, fileName));
            return res;
        }

        public async Task<IEnumerable<string>> GetAllFiles()
        {
            var res = Directory.GetFiles(_directoryPath, "*.txt").Select(Path.GetFileName);
            return res;
        }
    }
}
