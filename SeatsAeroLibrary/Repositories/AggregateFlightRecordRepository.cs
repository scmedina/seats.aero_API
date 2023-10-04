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

namespace SeatsAeroLibrary.Repositories
{
    public class AggregateFlightRecordRepository : IFlightRecordRepository
    {
        private readonly List<IFlightRecordRepository> _flightRecordRepositories = new List<IFlightRecordRepository>();

        public AggregateFlightRecordRepository()
        {
            _flightRecordRepositories.Add(new FlightRecordShortRepository());
            _flightRecordRepositories.Add(new FlightRecordLongRepository());
        }

        public void Add(FlightRecordDataModel entity)
        {
            foreach (IFlightRecordRepository repo in _flightRecordRepositories)
            {
                repo.Add(entity);
            }
        }

    }
}
