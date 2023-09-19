using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.FlightFactories
{
    public class SeatAvailabilityFilterFactory : IFlightFilterFactory
    {

        private SeatType _seatTypes;
        private int _minimumSeatsAvailable;
        public SeatAvailabilityFilterFactory(SeatType seatTypes, int minimumSeatsAvailable = 0)
        {
            _seatTypes = seatTypes;
            _minimumSeatsAvailable = minimumSeatsAvailable;
        }

        public IFlightFilter CreateFilter()
        {
            return new Models.FlightFilters.SeatAvailabilityFilter(_seatTypes, _minimumSeatsAvailable);
        }
    }
}
