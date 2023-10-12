using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Repositories
{
    public interface IFlightRecordRepository 
    {
        void Add(FlightRecordDataModel entity);
        public void Initialize(IConfigSettings configSettings, IDataAccess dataAccess);
    }
}
