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
    public class SeatsAeroAvailabilityAPI : SeatsAeroAPI<AvailabilityData>
    {
        public SeatsAeroAvailabilityAPI(Dictionary<string, string> queryParams = null) : base("availability", new string[] { "source" }, queryParams)
        {
        }
        public SeatsAeroAvailabilityAPI(MileageProgram mileageProgram, FilterAggregate filterAggregate) : base("availability", new string[] { "source" }, null)
        {
            Guard.AgainstMultipleSources(mileageProgram, nameof(mileageProgram));
            Guard.AgainstNull(filterAggregate, nameof(filterAggregate));

            this.QueryParams = new Dictionary<string, string>();
            this.QueryParams.Add("source", mileageProgram.ToString());

            DateTime startDate = (DateTime)DateFilter.GetDateVal(filterAggregate.Filters, isEndDate: false, DateTime.Today);
            DateTime? endDate = (DateTime)DateFilter.GetDateVal(filterAggregate.Filters, isEndDate: true);
            Guard.AgainstNull(endDate, nameof(endDate));
            Guard.AgainstInvalidDateRange(startDate,(DateTime)endDate, nameof(startDate),nameof(endDate));

            this.QueryParams.Add("start_date", startDate.ToString("yyyy-MM-dd"));
            this.QueryParams.Add("end_date", ((DateTime)endDate).ToString("yyyy-MM-dd"));
            this.QueryParams.Add("take", "10");
        }



        public class Route
        {
            public string ID { get; set; }
            public string OriginAirport { get; set; }
            public string OriginRegion { get; set; }
            public string DestinationAirport { get; set; }
            public string DestinationRegion { get; set; }
            public int NumDaysOut { get; set; }
            public int Distance { get; set; }
            public string Source { get; set; }
        }

        public class Flight
        {
            public string ID { get; set; }
            public string RouteID { get; set; }
            public Route Route { get; set; }
            public string Date { get; set; }
            public DateTime ParsedDate { get; set; }
            public bool YAvailable { get; set; }
            public bool? WAvailable { get; set; }
            public bool JAvailable { get; set; }
            public bool FAvailable { get; set; }
            public string YMileageCost { get; set; }
            public string? WMileageCost { get; set; }
            public string JMileageCost { get; set; }
            public string FMileageCost { get; set; }
            public int YRemainingSeats { get; set; }
            public int? WRemainingSeats { get; set; }
            public int JRemainingSeats { get; set; }
            public int FRemainingSeats { get; set; }
            public string YAirlines { get; set; }
            public string? WAirlines { get; set; }
            public string JAirlines { get; set; }
            public string FAirlines { get; set; }
            public bool YDirect { get; set; }
            public bool? WDirect { get; set; }
            public bool JDirect { get; set; }
            public bool FDirect { get; set; }
            public string Source { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
            public object AvailabilityTrips { get; set; }
        }

        public class AvailabilityData
        {
            public List<Flight> Data { get; set; }
            public int Count { get; set; }
            public bool HasMore { get; set; }
            public string MoreURL { get; set; }
            public int Cursor { get; set; }
        }
    }
}
