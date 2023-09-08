using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.FlightFilters
{
    public abstract class BasicSeatTypeFilter: BasicFilter
    {
        private SeatType _seatTypes;
        List<SeatType> _seatTypesList;

        public BasicSeatTypeFilter(SeatType seatTypes)
        {
            _seatTypes = seatTypes;

            EnumHelper enumHelper = new EnumHelper();
            _seatTypesList = enumHelper.GetBitFlagList(_seatTypes);
        }

        protected override bool FilterFlight(Flight flight)
        {

            foreach (SeatType seatType in _seatTypesList)
            {
                ClassAvailability classAvailability = flight.GetClassAvailability(seatType);
                if (FilterFlightBySeatType(flight,classAvailability))
                {
                    return true;
                }
            }
            return false;
        }

        protected abstract bool FilterFlightBySeatType(Flight flight, ClassAvailability classAvailability);
    }
}
