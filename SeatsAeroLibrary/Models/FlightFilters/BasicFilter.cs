using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.FlightFilters
{
    public abstract class BasicFilter : IFlightFilter
    {        
        List<Flight> IFlightFilter.Filter(List<Flight> flights)
        {
            return flights.Where(flight => FilterFlight(flight)).ToList();
        }
        protected abstract bool FilterFlight(Flight flight);
    }
}
