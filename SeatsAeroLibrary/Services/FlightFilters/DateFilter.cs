using NLog.Filters;
using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Models.Entities;
using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.FlightFilters
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
            return IsValidDate(flight.Date);
        }

        protected bool IsValidDate(DateTime? date)
        {
            if (date == null)
            {
                return false;
            }
            else if (IsEndDate)
            {
                return (DateTime)date <= Date;
            }
            else
            {
                return (DateTime)date >= Date;
            }
        }

        public static DateTime? GetDateVal(List<IFlightFilter> filters, out DateFilter dateFilter, bool isEndDate, DateTime? defaultVal = null)
        {
            List<DateFilter> dateFilters = new List<DateFilter>();
            if (FlightFiltersHelpers.GetFilters<DateFilter>(filters, ref dateFilters, df => df.IsEndDate == isEndDate))
            {
                dateFilter = dateFilters[0];
                return dateFilters[0].Date;
            }
            dateFilter = null;
            return defaultVal;
        }


        public static DateTime? GetDateVal(List<IFlightFilter> filters, bool isEndDate, DateTime? defaultVal = null)
        {
            DateFilter dateFilter = null;
            return GetDateVal(filters, out dateFilter, isEndDate, defaultVal);
        }
    }
}
