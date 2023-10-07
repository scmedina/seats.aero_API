using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeatsAeroLibrary.Models;

namespace SeatsAeroLibrary.Services.FlightFilters
{
    internal class MaxMileageCostFilter : BasicFilter
    {
        private int? _maxPoints;
        private bool _nonZero;

        public override string ToString()
        {
            return this.GetType().Name + ": " + _maxPoints.ToString();
        }

        public MaxMileageCostFilter( int? maxPoints = null, bool nonZero = true) : base()
        {
            _maxPoints = maxPoints;
            _nonZero = nonZero;
        }

        protected override bool FilterFlight(Flight flight)
        {
            if (_maxPoints is null)
            {
                return true;
            }
            else if (_nonZero == true && flight.MileageCost == 0)
            {
                return false;
            }
            return flight.MileageCost <= (int)_maxPoints;
        }
    }
}
