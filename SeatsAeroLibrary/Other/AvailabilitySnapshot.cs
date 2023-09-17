using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SeatsAeroLibrary.API.Models;
using SeatsAeroLibrary.Helpers;

namespace SeatsAeroLibrary.Models
{
    public class AvailabilitySnapshot
    {
        //private static string SnapshotFileName = "seats_aero_[source]_[dateStamp]_[timeStamp].json";
        private static string SnapshotFileName = "seats_aero_[source]_[dateStamp].json";
        private static string SnapshotDateFormat = "yyyyMMdd";
        private static string SnapshotTimeFormat = "HH";
        public static string SnapshotFileDirectory = $@"{Environment.GetEnvironmentVariable("Temp")}\\";
        public static int HoursRange = 4;

        public string GetFileNameBySourceAndDate(MileageProgram mileageProgram, DateTime currentTime)
        {
            return GetFileNameBySourceAndDate(SnapshotFileName, mileageProgram, currentTime);
        }

        public static string GetFileNameBySourceAndDate(string pattern, MileageProgram mileageProgram, DateTime currentTime)
        {
            string fileName = pattern.Replace("[source]", mileageProgram.ToString());
            fileName = fileName.Replace("[dateStamp]", currentTime.ToString(SnapshotDateFormat));
            fileName = fileName.Replace("[timeStamp]", currentTime.ToString(SnapshotTimeFormat));
            return fileName;
        }

        public void SaveSnapshot(MileageProgram mileageProgram, string results)
        {
            string filePath = SnapshotFileDirectory + GetFileNameBySourceAndDate(mileageProgram, DateTime.Now);
            System.IO.File.WriteAllText(filePath, results);
        }


        public void SaveSnapshot<T>(MileageProgram mileageProgram, List<T> results, string fileName)
        {
            string filePath = SnapshotFileDirectory + fileName;
            FileIO.ExportJsonFile(results, filePath);
        }

        public List<AvailabilityDataModel> LoadAvailabilityByFile(string filePath)
        {
            string json = FileIO.ReadFileContents(filePath);
            return LoadAvailabilityByFileContents(json);
        }

        public List<AvailabilityDataModel> LoadAvailabilityByFileContents(string json)
        {
            Guard.AgainstNullOrEmptyResultString(json, nameof(json));

            List<AvailabilityDataModel> availabilities = JsonSerializer.Deserialize<List<AvailabilityDataModel>>(json);
            return availabilities;
        }

        public bool TryFindValidSnapshot(MileageProgram mileageProgram, ref string results)
        {
            Guard.AgainstMultipleSources(mileageProgram, nameof(mileageProgram));

            bool success = false;
            results = "";
            DateTime currentTime = DateTime.Now;

            for (int i = 0; i <= HoursRange; i++)
            {
                string fileName = GetFileNameBySourceAndDate(mileageProgram, currentTime);

                List<string> filesFromToday = FileIO.GetFilesInDirectory(SnapshotFileDirectory, fileName);
                if (filesFromToday.Count > 0)
                {
                    results = FileIO.ReadFileContents(filesFromToday[0]);
                    success = true;
                }

                currentTime = currentTime.AddHours(-1);
            }

            return success;
        }

    }
}
