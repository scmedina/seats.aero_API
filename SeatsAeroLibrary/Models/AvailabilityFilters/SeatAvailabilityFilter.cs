using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.AvailabilityFilters
{
    public class SeatAvailabilityFilter : IFlightFilter

    {
        private SeatType _seatTypes;
        List<SeatType> seatTypesList;
        public SeatAvailabilityFilter(SeatType seatTypes) 
        {
            _seatTypes = seatTypes;

            EnumHelper enumHelper = new EnumHelper();
            seatTypesList = enumHelper.GetBitFlagList(_seatTypes);
        }

        List<Flight> IFlightFilter.Filter(List<Flight> flights)
        {
            throw new NotImplementedException();
        }
    }
}
