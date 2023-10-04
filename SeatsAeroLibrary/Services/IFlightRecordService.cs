using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Models.Entities;
using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeatsAeroLibrary.Repositories;

namespace SeatsAeroLibrary.Services
{
    public interface IFlightRecordService
    {
        public void AddRecord(Flight flight);
        public void AddRecords(List<Flight> flights);
        public void AddRepositoryType<TRepo>() where TRepo : IFlightRecordRepository, new();
        //public List<FlightRecord> GetAll();
    }
}
