using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiJenner.ServicesMAUI
{
    public interface IDataStorageText
    {
        /// <summary>
        /// Saves the specified text to the storage.
        /// </summary>
        /// <param name="fileName">The name of the file to save.</param>
        /// <param name="text">The text to save.</param>
        /// <returns>True if the save was successful; otherwise, false.</returns>
        bool SaveText(string fileName, string text);

        /// <summary>
        /// Reads the text from the specified file.
        /// </summary>
        /// <param name="fileName">The name of the file to read.</param>
        /// <returns>The read text.</returns>
        string ReadText(string fileName);

        /// <summary>
        /// Deletes the specified file from storage.
        /// </summary>
        /// <param name="fileName">The name of the file to delete.</param>
        /// <returns>True if the deletion was successful; otherwise, false.</returns>
        bool DeleteText(string fileName);

        /// <summary>
        /// Checks if the specified file exists in storage.
        /// </summary>
        /// <param name="fileName">The name of the file to check.</param>
        /// <returns>True if the file exists; otherwise, false.</returns>
        bool FileExists(string fileName);

        /// <summary>
        /// Gets a list of all text files in the storage.
        /// </summary>
        /// <returns>A list of file names.</returns>
        IEnumerable<string> GetAllFiles();
    }
}

