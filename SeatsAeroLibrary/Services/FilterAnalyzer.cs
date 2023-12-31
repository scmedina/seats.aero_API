﻿using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Services.FlightFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services
{
    public class FilterAnalyzer : IFilterAnalyzer
    {
        public static int DefaultDaysOutStart = 30;
        public static int DefaultDaysOutAfterStart = 30;

        public void AnalyzeFilters(FilterAggregate filter) 
        {
            Guard.AgainstNull(filter, nameof(filter));

            if (filter.Filters is null )
            {
                filter.Filters = new List<IFlightFilter>();
            }

            DateFilter startDateFilter, endDateFilter;
            DateTime startDate, endDate;

            startDate = (DateTime)DateFilter.GetDateVal(filter.Filters, out startDateFilter, isEndDate: false, DateTime.Today.AddDays(DefaultDaysOutStart));
            if (startDateFilter == null )
            {
                filter.Filters.Add(new DateFilter(startDate, isEndDate: false));
            }

            endDate = (DateTime)DateFilter.GetDateVal(filter.Filters, out endDateFilter, isEndDate: true, startDate.AddDays(DefaultDaysOutAfterStart));
            if (endDateFilter == null)
            {
                filter.Filters.Add(new DateFilter(endDate, isEndDate: true));
            }

        }
    }
}
