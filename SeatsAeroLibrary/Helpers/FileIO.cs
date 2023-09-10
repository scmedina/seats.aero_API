using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Collections;
using System.Reflection;

namespace SeatsAeroLibrary.Helpers
{
    public class FileIO
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

        public static void SaveStringToFile(string content, string filePath)
        {
            try
            {
                // Write the string content to the specified file path
                File.WriteAllText(filePath, content);
                Console.WriteLine($"String saved to {filePath} successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
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

        public static string ReadEmbeddedResource( string resourceName, Assembly assembly)
        {
            // Get the current assembly.
            if (assembly is null)
            {
                assembly = Assembly.GetExecutingAssembly();
            }

            // Construct the full resource name, including the namespace.
            string fullResourceName = $"{assembly.GetName().Name}.{resourceName}";

            // Read the embedded resource into a string.
            using (Stream stream = assembly.GetManifestResourceStream(fullResourceName))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException($"Resource '{fullResourceName}' not found.");
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
