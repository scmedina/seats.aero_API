﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.Entities
{
    public class Location : IEquatable<Location>
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

        public bool Equals(Location? other)
        {
            if (other is null) return false;
            if (other.AirportCode != AirportCode) return false;
            return true;
        }
    }
}
