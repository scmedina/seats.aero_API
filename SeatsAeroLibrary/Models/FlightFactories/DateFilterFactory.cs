using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.FlightFactories
{
    public class DateFilterFactory : IFlightFilterFactory
    {
        public DateTime Date { get; set; }
        public bool IsEndDate { get; set; }

        public DateFilterFactory(DateTime date, bool isEndDate = false)
        {
            Date = date;
            IsEndDate = isEndDate;
        }

        public IFlightFilter CreateFilter()
        {
            return new SeatsAeroLibrary.Models.FlightFilters.DateFilter(Date, IsEndDate);
        }
    }
}
