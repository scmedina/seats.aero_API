using SeatsAeroLibrary.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.FlightRecordID
{
    public interface IFlightRecordID : IComparable
    {
        public void Map(FlightRecordDataModel flightRecordDataModel);
    }
}
