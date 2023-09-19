using RestSharp;
using SeatsAeroLibrary.API.Models;
using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Models.FlightFilters;
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
    public class SeatsAeroCacheSearchAPI : SeatsAeroAPI<AvailabilityResultDataModel>
    {
        public SeatsAeroCacheSearchAPI(string originAirports, string destinationAirports,FilterAggregate filterAggregate) : base("partnerapi/search", new string[] { "origin_airport", "destination_airport" }, null)
        {
            Guard.AgainstNull(filterAggregate, nameof(filterAggregate));

            this.QueryParams = new Dictionary<string, string>();

            DateTime startDate = (DateTime)DateFilter.GetDateVal(filterAggregate.Filters, isEndDate: false, DateTime.Today);
            DateTime? endDate = (DateTime)DateFilter.GetDateVal(filterAggregate.Filters, isEndDate: true);
            Guard.AgainstNull(endDate, nameof(endDate));
            Guard.AgainstInvalidDateRange(startDate,(DateTime)endDate, nameof(startDate),nameof(endDate));

            this.QueryParams.Add("origin_airport", originAirports);
            this.QueryParams.Add("destination_airport", destinationAirports);
            this.QueryParams.Add("start_date", startDate.ToString("yyyy-MM-dd"));
            this.QueryParams.Add("end_date", ((DateTime)endDate).ToString("yyyy-MM-dd"));
            this.QueryParams.Add("take", "1000");
        }
    }
}
