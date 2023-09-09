using SeatsAeroLibrary.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.FlightFilters
{
    public class DateFilter : BasicFilter
    {
        public DateTime Date { get; set; }
        public bool IsEndDate { get; set; }

        public DateFilter(DateTime date, bool isEndDate = false)
        {
            Date = date;
            IsEndDate = isEndDate;
        }

        protected override bool FilterFlight(Flight flight)
        {
            if (IsEndDate)
            {
                return (flight.Date <= Date);
            }
            else
            {
                return (flight.Date >= Date);
            }
        }
    }
}
