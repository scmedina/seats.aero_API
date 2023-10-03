using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.DataModels
{
    public class FlightRecordDataModel
    {
        public Guid Id { get; set; }
        public string AvailabilityID { get; set; }
        public string OriginAirport { get; set; }
        public string OriginRegion { get; set; }
        public string DestinationAirport { get; set; }
        public string DestinationRegion { get; set; }
        public string SeatType { get; set; }
        public string DayOfWeek { get; set; }
        public bool Direct { get; set; }
        public int MileageCost { get; set; }
        public DateTime Date { get; set; }
        public string Airline { get; set; }
        public string Source { get; set; }

        public override string ToString()
        {
            return $"{AvailabilityID} {OriginAirport} {DestinationAirport} {SeatType} {DayOfWeek} {Direct} {MileageCost} {Date} {Airline} {Source}";
        }
    }
}
