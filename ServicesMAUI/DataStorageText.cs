namespace MiJenner.ServicesMAUI
{
    public class DataStorageText : IDataStorageText
    {
        private readonly string _directoryPath;

        public DataStorageText()
        {
            _directoryPath = FileSystem.Current.AppDataDirectory;
        }

        public bool SaveText(string fileName, string text)
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

        public string ReadText(string fileName)
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

        public bool DeleteText(string fileName)
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

        public bool FileExists(string fileName) => File.Exists(Path.Combine(_directoryPath, fileName));

        public IEnumerable<string> GetAllFiles() => Directory.GetFiles(_directoryPath, "*.txt").Select(Path.GetFileName);
    }

}
