using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Data;

namespace SeatsAeroLibrary.Models
{
    public class Route
    {
        [JsonPropertyName("ID")]
        public string Id { get; set; }

        [JsonPropertyName("OriginAirport")]
        public string OriginAirport { get; set; }

        [JsonPropertyName("OriginRegion")]
        public string OriginRegion { get; set; }

        [JsonPropertyName("DestinationAirport")]
        public string DestinationAirport { get; set; }

        [JsonPropertyName("DestinationRegion")]
        public string DestinationRegion { get; set; }

        [JsonPropertyName("NumDaysOut")]
        public int NumDaysOut { get; set; }

        [JsonPropertyName("Distance")]
        public int Distance { get; set; }

        [JsonPropertyName("Source")]
        public string Source { get; set; }


        public override string ToString()
        {
            return $"{OriginAirport},{OriginRegion} > {DestinationAirport},{DestinationRegion}";
        }
    }

}
