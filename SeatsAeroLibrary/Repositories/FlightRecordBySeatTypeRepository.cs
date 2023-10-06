using SeatsAeroLibrary.Services;
using SeatsAeroLibrary.Services.FlightRecordID;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Repositories
{
    public class FlightRecordBySeatTypeRepository : GenericFlightRecordRepository<FlightRecordBySeatTypeID>
    {
        public FlightRecordBySeatTypeRepository() : base()
        {
        }

        protected override string GetDefaultFilePath()
        {
            return $@"{_configSettings.OutputDirectory}\\Flight_Record_Lows_By_SeatType.csv";
        }
    }
}
