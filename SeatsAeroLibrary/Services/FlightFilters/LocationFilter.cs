using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.FlightFilters
{
    public class LocationFilter : BasicFilter
    {
        public List<LocationByType> Locations { get; set; }
        public bool IsDestination { get; set; }

        public LocationFilter(List<LocationByType> locations, bool isDestination)
        {
            Locations = locations;
            IsDestination = isDestination;
        }
        protected override bool FilterFlight(Flight flight)
        {
            if (Locations is null || Locations.Count == 0) return true;

            Location flightLocation = IsDestination ? flight.Route.Destination : flight.Route.Origin;

            foreach (LocationByType location in Locations)
            {
                string locationName = "";
                switch (location.Type)
                {
                    case Helpers.LocationType.Region:
                        locationName = flightLocation.Region;
                        break;
                    case Helpers.LocationType.Airport:
                        locationName = flightLocation.AirportCode;
                        break;
                }

                if (locationName.Trim().ToUpper() == location.Name.Trim().ToUpper())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
