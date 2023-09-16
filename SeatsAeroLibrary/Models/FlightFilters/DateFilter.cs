using NLog.Filters;
using SeatsAeroLibrary.Models.Entities;
using SeatsAeroLibrary.Services;
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

        public static DateTime? GetDateVal(List<IFlightFilter> filters, out DateFilter dateFilter, bool isEndDate, DateTime? defaultVal = null)
        {
            dateFilter = null;
            if (filters == null || filters.Count == 0)
            {
                return defaultVal;
            }

            dateFilter = filters.OfType<DateFilter>().FirstOrDefault(df => df.IsEndDate == isEndDate);
            DateTime? result = dateFilter?.Date ?? defaultVal;
            return result;
        }


        public static DateTime? GetDateVal(List<IFlightFilter> filters, bool isEndDate, DateTime? defaultVal = null)
        {
            DateFilter dateFilter = null;
            return GetDateVal(filters,out dateFilter, isEndDate, defaultVal);
        }

    }
}
