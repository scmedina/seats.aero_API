﻿using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Services;
using SeatsAeroLibrary.Services.FlightFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.FlightFactories
{
    public class SourceFilterFactory : IFlightFilterFactory
    {
        public string Sources { get; set; }

        public SourceFilterFactory(string sources)
        {
            Sources = sources;
        }

        public IFlightFilter CreateFilter()
        {
            return new SourceFilter(Sources);
        }

        public List<IFlightFilter> CreateFilters(SearchCriteriaDataModel searchCriteriaDataModel)
        {
            List<IFlightFilter> filters = new List<IFlightFilter>();
            throw new NotImplementedException();
            return filters;
        }
    }
}
