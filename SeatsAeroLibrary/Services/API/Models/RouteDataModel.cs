using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Data;

namespace SeatsAeroLibrary.Models
{
    public class RouteDataModel
    {
        public string ID { get; set; }
        public string OriginAirport { get; set; }
        public string OriginRegion { get; set; }
        public string DestinationAirport { get; set; }
        public string DestinationRegion { get; set; }
        public int NumDaysOut { get; set; }
        public int Distance { get; set; }
        public string Source { get; set; }

        public override string ToString()
        {
            return $"{OriginAirport},{OriginRegion} > {DestinationAirport},{DestinationRegion}";
        }
    }

}
