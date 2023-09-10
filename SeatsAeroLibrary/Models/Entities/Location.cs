using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.Entities
{
    public class Location
    {

        [JsonPropertyName("airportCode")]
        public string AirportCode  { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        public Location(string airportCode, string region)
        {
            AirportCode = airportCode;
            Region = region;
        }


        public override string ToString()
        {
            return $"{AirportCode} ({Region})";
        }
    }
}
