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
    public class SeatTypeFilter : BasicFilter
    {
        private SeatType _seatTypes;
        private List<SeatType> _seatTypesList;

        public SeatTypeFilter(SeatType seatTypes)
        {
            _seatTypes = seatTypes;

            EnumHelper enumHelper = new EnumHelper();
            _seatTypesList = enumHelper.GetBitFlagList(_seatTypes);
        }

        protected override bool FilterFlight(Flight flight)
        {
            return (_seatTypesList.Contains(flight.SeatType));
        }

    }
}
