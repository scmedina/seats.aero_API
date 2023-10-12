using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.Entities
{
    public class FlightRecord
    {
        public Guid Id { get; set; }
        public string AvailabilityID { get; set; }
        public string OriginAirport { get; set; }
        public string DestinationAirport { get; set; }
        public SeatType SeatType { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public bool Direct { get; set; }
        public int MileageCost { get; set; }
        public DateTime Date { get; set; }
        public string Airline { get; set; }
        public MileageProgram Source { get; set; }
        public string OriginRegion { get; internal set; }
        public string DestinationRegion { get; internal set; }


        public override string ToString()
        {
            return $"{AvailabilityID} {OriginAirport} {DestinationAirport} {SeatType} {DayOfWeek} {Direct} {MileageCost} {Date} {Airline} {Source}";
        }
    }
}
