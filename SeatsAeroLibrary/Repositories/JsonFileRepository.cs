using Autofac;
using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Repositories
{
    public abstract class JsonFileRepository<T,U> : FileRepository<T,U>
    {
        protected override List<T> LoadDataFromFile()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string jsonData = File.ReadAllText(_filePath);
                    return JsonSerializer.Deserialize<List<T>>(jsonData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data from file: {ex.Message}");
            }
            return new List<T>();
        }

        protected override void SaveDataToFile()
        {
            try
            {
                FileIO.ExportJsonFile<List<T>>(base.GetValueList(), _filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data to file: {ex.Message}");
            }
        }

    }
}
