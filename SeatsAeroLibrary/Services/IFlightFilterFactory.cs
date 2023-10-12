using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services
{
    public interface IFlightFilterFactory
    {
        IFlightFilter CreateFilter();
        List<IFlightFilter> CreateFilters(SearchCriteria searchCriteria);
    }
}
