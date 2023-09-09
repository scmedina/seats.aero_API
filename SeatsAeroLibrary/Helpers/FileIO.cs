using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Collections;

namespace SeatsAeroLibrary.Helpers
{
    internal class FileIO
    {
        internal static List<string> GetFilesInDirectory(string directoryPath, string filePattern)
        {
            List<string> fileList = new List<string>();

            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
                if (!directoryInfo.Exists)
                {
                    throw new DirectoryNotFoundException($"Directory not found: {directoryPath}");
                }

                fileList = directoryInfo.GetFiles(filePattern)
                                        .Select(file => file.FullName)
                                        .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return fileList;
        }

        internal static string ReadFileContents(string filePath)
        {
            try
            {
                string contents = File.ReadAllText(filePath);
                return contents;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public static void ExportJsonFile<T>(List<T> jsonClass,  string filePath)
        {
            string json = JsonSerializer.Serialize(jsonClass, new JsonSerializerOptions
            {
                WriteIndented = true // For pretty-printing the JSON
            });
            File.WriteAllText(filePath,json);
        }
    }
}
