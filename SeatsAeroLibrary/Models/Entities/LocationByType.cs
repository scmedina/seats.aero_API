using SeatsAeroLibrary.Helpers;
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
    }
}
