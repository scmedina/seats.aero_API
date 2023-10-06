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
        protected override List<T> LoadDataFromString(string data)
        {
            try
            {
                 return JsonSerializer.Deserialize<List<T>>(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data from file: {ex.Message}");
            }
            return new List<T>();
        }

        protected override string GetDataAsString(List<T> elements)
        {
            try
            {
                return FileIO.GetAsJsonString(elements);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data to file: {ex.Message}");
            }
            return "";
        }

    }
}
