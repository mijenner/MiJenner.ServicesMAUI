using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiJenner.ServicesMAUI
{
    public interface IDataStorageBin
    {
        /// <summary>
        /// Saves binary data to the specified file.
        /// </summary>
        /// <param name="fileName">The name of the file to save.</param>
        /// <param name="data">The binary data to save.</param>
        /// <returns>True if the save was successful; otherwise, false.</returns>
        bool SaveBinary(string fileName, byte[] data);

        /// <summary>
        /// Reads binary data from the specified file.
        /// </summary>
        /// <param name="fileName">The name of the file to read.</param>
        /// <returns>The read binary data.</returns>
        byte[] ReadBinary(string fileName);

        /// <summary>
        /// Deletes the specified file from storage.
        /// </summary>
        /// <param name="fileName">The name of the file to delete.</param>
        /// <returns>True if the deletion was successful; otherwise, false.</returns>
        bool DeleteBinary(string fileName);

        /// <summary>
        /// Checks if the specified file exists in storage.
        /// </summary>
        /// <param name="fileName">The name of the file to check.</param>
        /// <returns>True if the file exists; otherwise, false.</returns>
        bool FileExists(string fileName);

        /// <summary>
        /// Gets a list of all binary files in the storage.
        /// </summary>
        /// <returns>A list of file names.</returns>
        IEnumerable<string> GetAllFiles();
    }
}
