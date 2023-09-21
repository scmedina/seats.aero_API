using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Models.Entities;
using SeatsAeroLibrary.Services;
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

        public LocationFilterFactory(List<LocationByType> locations, bool isDestination)
        {
            Locations = locations;
            IsDestination = isDestination;
        }

        public IFlightFilter CreateFilter()
        {
            return new FlightFilters.LocationFilter(Locations, IsDestination);
        }

        public List<IFlightFilter> CreateFilters(SearchCriteriaDataModel searchCriteriaDataModel)
        {
            List<IFlightFilter> filters = new List<IFlightFilter>();
            throw new NotImplementedException();
            return filters;
        }
    }
}
