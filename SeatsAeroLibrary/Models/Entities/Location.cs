using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.Entities
{
    public class Location
    {
        public string AirportCode  { get; set; }
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
