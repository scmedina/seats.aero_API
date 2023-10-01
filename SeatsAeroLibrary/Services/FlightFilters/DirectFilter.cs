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

        public override string ToString()
        {
            return this.GetType().Name + ": " + _direct.ToString();
        }

        public DirectFilter(bool? direct = null) : base()
        {
            _direct = direct;
        }

        protected override bool FilterFlight(Flight flight)
        {
            return IsValidDirect(flight.Direct);
        }

        private bool IsValidDirect(bool? direct)
        {
            if (_direct is null)
            {
                return true;
            }
            else if (direct is null)
            {
                return false;
            }
            return ((bool)direct == (bool)_direct);
        }
    }
}
