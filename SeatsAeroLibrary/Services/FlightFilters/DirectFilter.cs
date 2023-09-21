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
    public class DirectFilter : BasicFilter
    {
        private bool? _direct;

        public DirectFilter(bool? direct = null) : base()
        {
            _direct = direct;
        }

        protected override bool FilterFlight(Flight flight)
        {
            if (_direct is null)
            {
                return true;
            }
            return flight.Direct == (bool)_direct;
        }
    }
}
