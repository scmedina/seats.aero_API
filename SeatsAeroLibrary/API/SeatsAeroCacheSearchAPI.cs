using NLog.Filters;
using RestSharp;
using SeatsAeroLibrary.API.Models;
using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Services.FlightFilters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static SeatsAeroLibrary.API.SeatsAeroAvailabilityAPI;

namespace SeatsAeroLibrary.API
{
    public class SeatsAeroCacheSearchAPI : SeatsAeroAPI<AvailabilityResultDataModel, List<Flight>>
    {
        public FilterAggregate FilterAggregate { get; set; }
        public string OriginAirports { get; set; }
        public string DestinationAirports { get; set; }

        public SeatsAeroCacheSearchAPI(FilterAggregate filterAggregate) : base("partnerapi/search", new string[] { "origin_airport", "destination_airport" }, null)
        {
            Guard.AgainstNull(filterAggregate, nameof(filterAggregate));

            this.FilterAggregate = filterAggregate;

            this.QueryParams = new Dictionary<string, string>();

            DateTime startDate = (DateTime)DateFilter.GetDateVal(filterAggregate.Filters, isEndDate: false, DateTime.Today);
            DateTime? endDate = (DateTime)DateFilter.GetDateVal(filterAggregate.Filters, isEndDate: true);
            Guard.AgainstNull(endDate, nameof(endDate));
            Guard.AgainstInvalidDateRange(startDate,(DateTime)endDate, nameof(startDate),nameof(endDate));


            List<LocationFilter> originLocations = new List<LocationFilter>();
            Guard.AgainstFailure(FlightFiltersHelpers.GetFilters<LocationFilter>(filterAggregate.Filters, ref originLocations, df => df.IsDestination = false), "Origin flights");
            OriginAirports = string.Join(", ", originLocations.SelectMany(filter => filter.Locations).Select(location => location.Name));

            List<LocationFilter> destinationLocations = new List<LocationFilter>();
            Guard.AgainstFailure(FlightFiltersHelpers.GetFilters<LocationFilter>(filterAggregate.Filters, ref originLocations, df => df.IsDestination = true), "Origin flights");
            DestinationAirports = string.Join(", ", destinationLocations.SelectMany(filter => filter.Locations).Select(location => location.Name));

            this.QueryParams.Add("origin_airport", OriginAirports);
            this.QueryParams.Add("destination_airport", DestinationAirports);
            this.QueryParams.Add("start_date", startDate.ToString("yyyy-MM-dd"));
            this.QueryParams.Add("end_date", ((DateTime)endDate).ToString("yyyy-MM-dd"));
            this.QueryParams.Add("take", "1000");
        }

        protected override List<Flight> GetU(AvailabilityResultDataModel? data)
        {
            return Flight.GetFilteredFlights(FilterAggregate, data?.data);
        }
    }
}
