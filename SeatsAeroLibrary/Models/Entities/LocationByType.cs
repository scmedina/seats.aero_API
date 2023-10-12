using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.Entities
{
    public class LocationByType
    {
        public string Name { get; set; }
        public LocationType Type { get; set; }

        public LocationByType(string name, LocationType type) 
        { 
            Name = name;
            Type = type;
        }


        public LocationByType(string airportCode) : this(airportCode, LocationType.Airport) { }

        public LocationByType(RegionName value) 
        {
            Name = AttributeHelper.GetDescription(value, value.ToString());
            Type = LocationType.Region;
        }

        public static List<LocationByType> GetAirportsFromString(string airports)
        {
            List<LocationByType> results = new List<LocationByType>();
            string[] airportsArray = airports.Split(',');
            foreach (string airport in airportsArray)
            {
                if (string.IsNullOrEmpty(airport))
                {
                    continue;
                }
                results.Add(new LocationByType(airport, LocationType.Airport));
            }
            return results;
        }
    }
}
