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
        private List<SeatType> _seatTypesList;
        private bool _enforceSeatCount;

        public BasicSeatTypeFilter(SeatType seatTypes, bool enforceSeatCount = false)
        {
            _seatTypes = seatTypes;
            _enforceSeatCount = enforceSeatCount;

            EnumHelper enumHelper = new EnumHelper();
            _seatTypesList = enumHelper.GetBitFlagList(_seatTypes);
        }

        protected override bool FilterFlight(Flight flight)
        {
            if (_enforceSeatCount)
            {
                SourceDetailsAttribute details = SourceDetailsAttribute.GetDetails(flight.Source);
                if (details != null)
                {
                    if (details.HasSeatCount == false)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else if (_seatTypesList.Contains(flight.SeatType) == false)
            {
                return false;
            }

            return FilterFlightBySeatType(flight);
        }

        protected abstract bool FilterFlightBySeatType(Flight flight);
    }
}
