﻿using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Models.Entities;
using SeatsAeroLibrary.Services;
using SeatsAeroLibrary.Services.FlightFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.FlightFactories
{
    public class SeatAvailabilityFilterFactory : IFlightFilterFactory
    {
        private int _minimumSeatsAvailable;

        public SeatAvailabilityFilterFactory(int minimumSeatsAvailable = 0)
        {
            _minimumSeatsAvailable = minimumSeatsAvailable;
        }

        public IFlightFilter CreateFilter()
        {
            return new FlightFilters.SeatAvailabilityFilter(_minimumSeatsAvailable);
        }

        public List<IFlightFilter> CreateFilters(SearchCriteria searchCriteria)
        {
            List<IFlightFilter> filters = new List<IFlightFilter>();
            filters.Add(new SeatAvailabilityFilter(searchCriteria.MinimumSeats));
            return filters;
        }
    }
}
