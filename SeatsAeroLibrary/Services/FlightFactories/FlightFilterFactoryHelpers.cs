using SeatsAeroLibrary.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.FlightFactories
{
    public class FlightFilterFactoryHelpers
    {

        public static List<IFlightFilter> GetFilters<TFactory>(SearchCriteria searchCriteria) where TFactory : IFlightFilterFactory, new()
        {
            IFlightFilterFactory factory = new TFactory();
            return factory.CreateFilters(searchCriteria);
        }
    }
}
