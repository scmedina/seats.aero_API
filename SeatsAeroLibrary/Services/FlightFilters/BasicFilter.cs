using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.FlightFilters
{
    public abstract class BasicFilter : IFlightFilter
    {
        public List<Flight> Filter(List<Flight> flights)
        {
            return flights.Where(flight => FilterFlight(flight)).ToList();
        }
        protected abstract bool FilterFlight(Flight flight);
    }
}
