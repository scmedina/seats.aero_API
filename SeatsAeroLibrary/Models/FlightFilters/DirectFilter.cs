using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.FlightFilters
{
    public class DirectFilter : BasicSeatTypeFilter
    {
        private bool? _direct;

        public DirectFilter(SeatType seatTypes, bool? direct = null) : base(seatTypes)
        {
            _direct = direct;
        }

        protected override bool FilterFlightBySeatType(Flight flight, ClassAvailability classAvailability)
        {
            if (_direct is null)
            {
                return true;
            }
            return classAvailability.Direct == (bool)_direct ;
        }
    }
}
