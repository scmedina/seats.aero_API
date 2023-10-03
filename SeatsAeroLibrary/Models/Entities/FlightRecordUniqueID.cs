using SeatsAeroLibrary.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.Entities
{

    public class FlightRecordUniqueID 
    {
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

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + (OriginAirport?.GetHashCode() ?? 0);
            hash = hash * 23 + (DestinationAirport?.GetHashCode() ?? 0);
            hash = hash * 23 + (SeatType?.GetHashCode() ?? 0);
            hash = hash * 23 + (DayOfWeek?.GetHashCode() ?? 0);
            hash = hash * 23 + (Direct.GetHashCode());
            hash = hash * 23 + (Date.GetHashCode());
            hash = hash * 23 + (Airline?.GetHashCode() ?? 0);
            hash = hash * 23 + (Source?.GetHashCode() ?? 0);
            return hash;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            FlightRecordUniqueID other = (FlightRecordUniqueID)obj;
            return OriginAirport == other.OriginAirport && 
                DestinationAirport == other.DestinationAirport &&
                SeatType == other.SeatType &&
                DayOfWeek == other.DayOfWeek &&
                Direct == other.Direct &&
                Date == other.Date &&
                Airline == other.Airline &&
                Source == other.Source;
        }
    }
}
