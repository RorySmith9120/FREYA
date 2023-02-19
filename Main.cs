using System;
using System.IO;

namespace ForensicFileRecovery
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set the directory to search for files
            string searchDirectory = "C:\\";

            // Get all files in the search directory
            string[] allFiles = Directory.GetFiles(searchDirectory, "*.*", SearchOption.AllDirectories);

            // Iterate over all files and search for deleted files
            foreach (string file in allFiles)
            {
                // Check if the file is a deleted file
                if (IsDeletedFile(file))
                {
                    // The file is a deleted file, so recover it
                    RecoverDeletedFile(file);
                }
            }
        }

        // This method checks if a given file is a deleted file
        static bool IsDeletedFile(string file)
        {
            // Read the first byte of the file
            byte fileHeader;
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                fileHeader = (byte)fs.ReadByte();
            }

            // Check if the first byte of the file is the deleted file signature
            return fileHeader == 0xE5;
        }

        // This method recovers a deleted file
        static void RecoverDeletedFile(string file)
        {
            // Set the name for the recovered file
            string recoveredFileName = "recovered_" + Path.GetFileName(file);

            // Copy the file to the new location
            File.Copy(file, Path.Combine(Path.GetDirectoryName(file), recoveredFileName));
        }
    }
}