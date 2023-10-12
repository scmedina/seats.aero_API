using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Collections;
using System.Reflection;
using System.Data;

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

        public static void ExportJsonFile<T>(T jsonClass, string filePath)
        {
            File.WriteAllText(filePath, GetAsJsonString(jsonClass));
        }

        public static string GetAsJsonString<T>(T jsonClass)
        {
            string json = JsonSerializer.Serialize(jsonClass, new JsonSerializerOptions
            {
                WriteIndented = true // For pretty-printing the JSON
            });
            return json;
        }

        public static void ExportToCsv(DataTable dataTable, string filePath)
        {
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                Console.WriteLine("DataTable is empty. Nothing to export.");
                return;
            }

            try
            {
                // Create a StreamWriter to write the CSV file
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    // Write the column headers
                    sw.WriteLine(string.Join(",", dataTable.Columns.Cast<DataColumn>().Select(col => col.ColumnName)));

                    // Write the data rows
                    foreach (DataRow row in dataTable.Rows)
                    {
                        var fields = row.ItemArray.Select(field => field.ToString());
                        sw.WriteLine("\"" +string.Join("\",\"", fields)+"\"");
                    }

                    Console.WriteLine($"DataTable exported to: {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while exporting to CSV: {ex.Message}");
            }
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
