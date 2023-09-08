using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.FlightFactories
{
    public class MaxMileageCostFilterFactory : IFlightFilterFactory
    {

        private SeatType _seatTypes;
        private int? _maxPoints;
        private bool _nonZero;

        public MaxMileageCostFilterFactory(SeatType seatTypes, int? maxPoints = null, bool nonZero = false)
        {
            _seatTypes = seatTypes;
            _maxPoints = maxPoints;
            _nonZero = nonZero;
        }

        public IFlightFilter CreateFilter()
        {
            return new SeatsAeroLibrary.Models.FlightFilters.MaxMileageCostFilter(_seatTypes, _maxPoints, _nonZero);
        }
    }
}
