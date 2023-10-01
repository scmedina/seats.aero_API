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
    public class SeatAvailabilityFilter : BasicFilter

    {
        private int _minimumSeatsAvailable;

        public override string ToString()
        {
            return this.GetType().Name + ": "+ _minimumSeatsAvailable.ToString();
        }

        public SeatAvailabilityFilter(int minimumSeatsAvailable = 0) 
        {
            _minimumSeatsAvailable = minimumSeatsAvailable;
        }

        protected override bool FilterFlight(Flight flight)
        {

            SourceDetailsAttribute details = SourceDetailsAttribute.GetDetails(flight.Source);
            if (details != null)
            {
                if (details.HasSeatCount == false)
                {
                    return true;
                }
            }
            return flight.Available = true && flight.RemainingSeats >= _minimumSeatsAvailable;
        }

    }
}
