using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Repositories
{
    public abstract class CsvFileRepository<T,U> : FileRepository<T, U> where T: ICsvExport
    {
        protected override List<T> LoadDataFromFile()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string csvData = File.ReadAllText(_filePath);
                    //return JsonSerializer.Deserialize<List<T>>(csvData);
                    throw new NotImplementedException();
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
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data to file: {ex.Message}");
            }
        }
    }
}
