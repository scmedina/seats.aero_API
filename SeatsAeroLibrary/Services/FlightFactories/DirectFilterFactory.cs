using SeatsAeroLibrary.Models;
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
    }
}
