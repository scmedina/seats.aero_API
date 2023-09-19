using SeatsAeroLibrary.Models.FlightFilters;
using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.FlightFactories
{
    public class SourceFilterFactory : IFlightFilterFactory
    {
        public string Sources { get; set; }

        public SourceFilterFactory(string sources) 
        { 
            Sources = sources;
        }

        public IFlightFilter CreateFilter()
        {
            return new SourceFilter(Sources);
        }
    }
}
