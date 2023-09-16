using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Data;

namespace SeatsAeroLibrary.Models
{
    public class AvailabilityDataModel
    {
        [JsonPropertyName("ID")]
        public string Id { get; set; }

        [JsonPropertyName("RouteID")]
        public string RouteId { get; set; }

        [JsonPropertyName("Route")]
        public RouteDataModel Route { get; set; }

        [JsonPropertyName("Date")]
        public string Date { get; set; }

        [JsonPropertyName("ParsedDate")]
        public DateTime ParsedDate { get; set; }

        [JsonPropertyName("YAvailable")]
        public bool? YAvailable { get; set; }

        [JsonPropertyName("WAvailable")]
        public bool? WAvailable { get; set; }

        [JsonPropertyName("JAvailable")]
        public bool? JAvailable { get; set; }

        [JsonPropertyName("FAvailable")]
        public bool? FAvailable { get; set; }

        [JsonPropertyName("YMileageCost")]
        public string YMileageCost { get; set; }

        [JsonPropertyName("WMileageCost")]
        public string WMileageCost { get; set; }

        [JsonPropertyName("JMileageCost")]
        public string JMileageCost { get; set; }

        [JsonPropertyName("FMileageCost")]
        public string FMileageCost { get; set; }

        [JsonPropertyName("YRemainingSeats")]
        public int? YRemainingSeats { get; set; }

        [JsonPropertyName("WRemainingSeats")]
        public int? WRemainingSeats { get; set; }

        [JsonPropertyName("JRemainingSeats")]
        public int? JRemainingSeats { get; set; }

        [JsonPropertyName("FRemainingSeats")]
        public int? FRemainingSeats { get; set; }

        [JsonPropertyName("YAirlines")]
        public string YAirlines { get; set; }

        [JsonPropertyName("WAirlines")]
        public string WAirlines { get; set; }

        [JsonPropertyName("JAirlines")]
        public string JAirlines { get; set; }

        [JsonPropertyName("FAirlines")]
        public string FAirlines { get; set; }

        [JsonPropertyName("YDirect")]
        public bool? YDirect { get; set; }

        [JsonPropertyName("WDirect")]
        public bool? WDirect { get; set; }

        [JsonPropertyName("JDirect")]
        public bool? JDirect { get; set; }

        [JsonPropertyName("FDirect")]
        public bool? FDirect { get; set; }

        [JsonPropertyName("Source")]
        public string Source { get; set; }

        [JsonPropertyName("ComputedLastSeen")]
        public string ComputedLastSeen { get; set; }

        [JsonPropertyName("APITermsOfUse")]
        public string APITermsOfUse { get; set; }

        [JsonPropertyName("AvailabilityTrips")]
        public object AvailabilityTrips { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Date: {Date}, YAirlines: {YAirlines}, YAvailable: {YAvailable}, JAvailable: {JAvailable}";
        }
    }


}
