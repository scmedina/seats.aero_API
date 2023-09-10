using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.FlightFilters
{
    internal class MaxMileageCostFilter : BasicSeatTypeFilter
    {
        private int? _maxPoints;
        private bool _nonZero;

        public MaxMileageCostFilter(SeatType seatTypes, int? maxPoints = null, bool nonZero = false) : base(seatTypes)
        {
            _maxPoints = maxPoints;
            _nonZero = nonZero;
        }

        protected override bool FilterFlightBySeatType(Flight flight)
        {
            if (_maxPoints is null)
            {
                return true;
            }
            else if (_nonZero == true && flight.MileageCost== 0)
            {
                return false;
            }
            return flight.MileageCost <= (int)_maxPoints;
        }
    }
}
