namespace MiJenner.ServicesMAUI
{
    public class FilePathService
    {
        private readonly string baseDirectory;

        public FilePathService()
        {
            baseDirectory = FileSystem.AppDataDirectory;
        }

        /// <summary>
        /// Examples of usage: 
        /// var filePathService = new FilePathService();
        /// string settingsPath = filePathService.GetFilePath(FileType.Settings);
        /// string customImagePath = filePathService.GetFilePath(FileType.Image, "customImage.jpg")
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="overrideFileName"></param>
        /// <returns></returns>
        public string GetFilePath(FileTypeEnum fileType, string? overrideFileName = null)
        {
            string fileName = overrideFileName ?? GetDefaultFileName(fileType);
            return Path.Combine(baseDirectory, fileName);
        }

        private string GetDefaultFileName(FileTypeEnum fileType)
        {
            return fileType switch
            {
                FileTypeEnum.Settings => "settings.json",
                FileTypeEnum.Image => "image.png",
                FileTypeEnum.Text => "document.txt",
                FileTypeEnum.Other => "miscfile.dat",
                _ => "defaultfile.dat"
            };
        }
    }
}
