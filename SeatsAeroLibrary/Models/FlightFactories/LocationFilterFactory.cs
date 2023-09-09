﻿using SeatsAeroLibrary.Models.Entities;
using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.FlightFactories
{
    public class LocationFilterFactory : IFlightFilterFactory
    {
        public List<LocationByType> Locations { get; set; }
        public bool IsDestination { get; set; }

        public LocationFilterFactory(List<LocationByType> locations, bool isDestination)
        {
            Locations = locations;
            IsDestination = isDestination;
        }

        public IFlightFilter CreateFilter()
        {
            return new SeatsAeroLibrary.Models.FlightFilters.LocationFilter(Locations, IsDestination);
        }
    }
}
