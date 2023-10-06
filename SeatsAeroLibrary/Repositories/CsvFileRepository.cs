using CsvHelper;
using CsvHelper.Configuration;
using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Repositories
{
    public abstract class CsvFileRepository<T, U> : FileRepository<T, U>
    {
        protected override List<T> LoadDataFromString(string data)
        {
            List<T> results = new List<T>(); 
            try
            {
                using (var reader = new StringReader(data))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    results = csv.GetRecords<T>().ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data from file: {ex.Message}");
            }
            return results;
        }

        protected override string GetDataAsString(List<T> elements)
        {
            string result = "";
            try
            {
                // Writing list to CSV string
                using (var writer = new StringWriter())
                using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    csv.WriteRecords(elements);
                    writer.Flush();
                    result = writer.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data to file: {ex.Message}");
            }
            return result;
        }
    }
}
