using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.FlightFactories
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
            return new SeatsAeroLibrary.Models.FlightFilters.DirectFilter(_seatTypes, _direct);
        }
    }
}
