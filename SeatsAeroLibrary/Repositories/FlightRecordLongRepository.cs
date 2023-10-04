using SeatsAeroLibrary.Models.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SeatsAeroLibrary.Profiles;
using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Services.FlightRecordID;
using SeatsAeroLibrary.Services;

namespace SeatsAeroLibrary.Repositories
{
    public class FlightRecordLongRepository : GenericFlightRecordRepository<FlightRecordLongID>
    {
        public FlightRecordLongRepository() : base()
        {
        }

        protected override string GetDefaultFilePath()
        {
            return $@"{_configSettings.OutputDirectory}\\Flight_Record_Lows.json";
        }
    }
}
