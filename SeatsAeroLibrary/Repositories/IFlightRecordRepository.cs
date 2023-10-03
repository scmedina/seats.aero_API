using SeatsAeroLibrary.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Repositories
{
    public interface IFlightRecordRepository: IRepository<FlightRecordDataModel, Guid>
    {
    }
}
