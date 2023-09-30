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
    public class LocationFilterFactory : IFlightFilterFactory
    {
        public List<LocationByType> Locations { get; set; }
        public bool IsDestination { get; set; }

        public LocationFilterFactory() { }

        public LocationFilterFactory(List<LocationByType> locations, bool isDestination)
        {
            Locations = locations;
            IsDestination = isDestination;
        }

        public LocationFilterFactory(string airports, bool isDestination) : this(GetAirports(airports), isDestination) { }

        public IFlightFilter CreateFilter()
        {
            return new FlightFilters.LocationFilter(Locations, IsDestination);
        }

        private static List<LocationByType> GetAirports(string airports)
        {
            if (String.IsNullOrWhiteSpace(airports) == false)
            {
                return LocationByType.GetAirportsFromString(airports);
            }
            return null;
        }

        public List<IFlightFilter> CreateFilters(SearchCriteria searchCriteria)
        {
            List<IFlightFilter> filters = new List<IFlightFilter>();

            AddAirports(searchCriteria.OriginAirports, false, filters);
            AddAirports(searchCriteria.DestinationAirports, true, filters);

            return filters;
        }

        private void AddAirports(string airports, bool isDestination, List<IFlightFilter> filters)
        {
            List<LocationByType> locations = GetAirports(airports);
            if (locations != null && locations.Count > 0)
            {
                filters.Add(new LocationFilter(locations, isDestination));
            }
        }
    }
}
