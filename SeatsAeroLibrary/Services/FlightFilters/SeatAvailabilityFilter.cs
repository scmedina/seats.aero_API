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
    public class SeatAvailabilityFilter : BasicSeatTypeFilter

    {
        private int _minimumSeatsAvailable;

        public SeatAvailabilityFilter(SeatType seatTypes, int minimumSeatsAvailable = 0) : base(seatTypes)
        {
            _minimumSeatsAvailable = minimumSeatsAvailable;
        }
        protected override bool FilterFlightBySeatType(Flight flight)
        {
            return flight.Available = true && flight.RemainingSeats >= _minimumSeatsAvailable;
        }
    }
}
