using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Data;
using SeatsAeroLibrary.Models.Entities;

namespace SeatsAeroLibrary.Models
{
    public class AvailabilityDataModel
    {
        public string ID { get; set; }
        public string RouteID { get; set; }
        public RouteDataModel Route { get; set; }
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

        public override string ToString()
        {
            return $"ID: {ID}, Date: {Date}, YAirlines: {YAirlines}, YAvailable: {YAvailable}, JAvailable: {JAvailable}";
        }
    }


}
