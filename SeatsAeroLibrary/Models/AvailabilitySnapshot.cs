using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeatsAeroLibrary.Helpers;

namespace SeatsAeroLibrary.Models
{
    internal class FileSnapshot
    {
        private static string SnapshotFileName = "seats_aero_[source]_[dateStamp]_[timeStamp]";
        private static string SnapshotDateFormat = "yyyyMMdd";
        private static string SnapshotTimeFormat = "HH";
        public static string SnapshotFileDirectory = $@"{Environment.GetEnvironmentVariable("Temp")}\\";
        public static int HoursRange = 4;

        public string GetFileNameBySourceAndDate(MileageProgram mileageProgram, DateTime currentTime)
        {
            string fileName = SnapshotFileName.Replace("[source]", mileageProgram.ToString());
            fileName = fileName.Replace("[dateStamp]", currentTime.ToString(SnapshotDateFormat));
            fileName = fileName.Replace("[timeStamp]", currentTime.ToString(SnapshotTimeFormat));
            return fileName;
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
