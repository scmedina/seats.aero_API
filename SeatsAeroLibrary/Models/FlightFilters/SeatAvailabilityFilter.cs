using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.FlightFilters
{
    public class SeatAvailabilityFilter : IFlightFilter

    {
        private SeatType _seatTypes;
        private int _minimumSeatsAvailable;
        List<SeatType> seatTypesList;
        public SeatAvailabilityFilter(SeatType seatTypes, int minimumSeatsAvailable = 0) 
        {
            _seatTypes = seatTypes;
            _minimumSeatsAvailable = minimumSeatsAvailable;

            EnumHelper enumHelper = new EnumHelper();
            seatTypesList = enumHelper.GetBitFlagList(_seatTypes);
        }

        List<Flight> IFlightFilter.Filter(List<Flight> flights)
        {
            List<Flight> results = new List<Flight>(flights);
            foreach (SeatType seatType in seatTypesList)
            {
                results = results.Where(flight => flight.GetClassAvailability(seatType).RemainingSeats >= _minimumSeatsAvailable).ToList();
            }
            return null;
            //return flights.Where(flight => flight.)
        }
    }
}
