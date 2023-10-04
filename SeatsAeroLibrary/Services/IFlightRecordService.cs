using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Models.Entities;
using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services
{
    public interface IFlightRecordService
    {
        public void AddRecord(Flight flight);
        public void AddRecords(List<Flight> flights);
        //public List<FlightRecord> GetAll();
    }
}
