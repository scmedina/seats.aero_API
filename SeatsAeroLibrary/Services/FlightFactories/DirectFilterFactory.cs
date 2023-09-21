using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.FlightFactories
{
    public class DirectFilterFactory : IFlightFilterFactory
    {

        private SeatType _seatTypes;
        private bool? _direct;
        public DirectFilterFactory(SeatType seatTypes, bool? direct = null)
        {
            _seatTypes = seatTypes;
            _direct = direct;
        }

        public IFlightFilter CreateFilter()
        {
            return new FlightFilters.DirectFilter(_seatTypes, _direct);
        }

        public List<IFlightFilter> CreateFilters(SearchCriteriaDataModel searchCriteriaDataModel)
        {
            List<IFlightFilter> filters = new List<IFlightFilter>();
            throw new NotImplementedException();
            return filters;
        }
    }
}
