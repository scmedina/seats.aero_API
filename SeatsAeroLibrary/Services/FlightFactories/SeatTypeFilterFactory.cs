using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Models.Entities;
using SeatsAeroLibrary.Services.FlightFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.FlightFactories
{
    public class SeatTypeFilterFactory : IFlightFilterFactory
    {
        private SeatType _seatTypes;


        public SeatTypeFilterFactory() { }

        public SeatTypeFilterFactory(SeatType seatTypes)
        {
            _seatTypes = seatTypes;
        }

        public IFlightFilter CreateFilter()
        {
            return new FlightFilters.SeatTypeFilter(_seatTypes);
        }

        public List<IFlightFilter> CreateFilters(SearchCriteria searchCriteria)
        {
            List<IFlightFilter> filters = new List<IFlightFilter>();
            filters.Add(new SeatTypeFilter(searchCriteria.SeatTypeEnum));
            return filters;
        }
    }
}
