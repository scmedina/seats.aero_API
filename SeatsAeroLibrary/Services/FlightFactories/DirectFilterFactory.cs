using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Models.Entities;
using SeatsAeroLibrary.Services;
using SeatsAeroLibrary.Services.FlightFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.FlightFactories
{
    public class DirectFilterFactory : IFlightFilterFactory
    {
        private bool? _direct;

        public DirectFilterFactory() { }

        public DirectFilterFactory(bool? direct = null)
        {
            _direct = direct;
        }

        public IFlightFilter CreateFilter()
        {
            return new FlightFilters.DirectFilter(_direct);
        }

        public List<IFlightFilter> CreateFilters(SearchCriteria searchCriteria)
        {
            List<IFlightFilter> filters = new List<IFlightFilter>();
            filters.Add(new DirectFilter(searchCriteria.Direct));
            return filters;
        }
    }
}
