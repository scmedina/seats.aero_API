using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeatsAeroLibrary.Helpers;

namespace SeatsAeroLibrary
{
    internal class FileSnapshot
    {
        private static string SnapshotFileName = "seats_aero_[source]_[dateStamp]_[timeStamp]";
        private static string SnapshotDateFormat = "yyyyMMdd";
        private static string SnapshotTimeFormat = "HH";
        public static string SnapshotFileDirectory = $@"{Environment.GetEnvironmentVariable("Temp")}\\";

        public string GetFileNameBySourceAndDate(MileageProgram mileageProgram, DateTime currentTime)
        {
            string fileName = SnapshotFileName.Replace("[source]", mileageProgram.ToString());
            fileName = fileName.Replace("[dateStamp]", DateTime.Today.ToString(SnapshotDateFormat));
            fileName = fileName.Replace("[timeStamp]", DateTime.Now.ToString(SnapshotTimeFormat));
            return fileName;
        }

        public bool TryFindValidSnapshot(MileageProgram mileageProgram, ref string results)
        {
            MileageProgramHelpers.CheckForSingleMileageProgram(mileageProgram);

            bool success = false;
            results = "";

            string fileName = GetFileNameBySourceAndDate(mileageProgram, DateTime.Now);

            List<string> filesFromToday = FileIO.GetFilesInDirectory(SnapshotFileDirectory, fileName);
            if (filesFromToday.Count <= 0)
            {
                return success;
            }

            results = FileIO.ReadFileContents(filesFromToday[0]);
            success = true;

            return success;
        }

    }
}
