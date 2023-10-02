using SeatsAeroLibrary.API.Models;
using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models
{
    public class Flight : IEquatable<Route>, IEquatable<Flight>, IComparable<Flight>
    {
        public Route Route { get; set; }
        public string ID { get; set; }
        public string RouteString
        {
            get { return Route.ToString(); }
        }
        public DateTime Date { get; set; }
        public string DateString
        {
            get { return Date.ToString("d"); }
        }
        private string ComputedLastSeen { get; set; }
        private object AvailabilityTrips { get; set; }
        public MileageProgram Source { get; set; }
        public string SourceString
        {
            get { return Source.ToString(); }
        }
        public SeatType SeatType { get; set; }
        public string SeatTypeString
        {
            get { return SeatType.ToString(); }
        }
        public bool Available { get; set; }
        public int RemainingSeats { get; set; }
        public string Airlines { get; set; }
        public bool Direct { get; set; }
        public int MileageCost { get; set; }

        public decimal MileageCostUSD
        {
            get
            {
                return MileageCost / 100;
            }
        }

        public override string ToString()
        {
            return $"{ID}, {Route.Origin}, {Route.Destination}, {SeatType}, {DateString}, {Date.DayOfWeek}, {RemainingSeats}, {Direct}, {MileageCost}, {MileageCostUSD.ToString("C")}, \"{Airlines.Replace(",",";")}\", {Source}";
        }

        public static string GetHeaderString()
        {
            return $"ID, Origin, Destination, Seat Type, Date, Day Of Week, Remaining Seats, Direct, Mileage Cost, Cost Equivalent $, Airlines, Source";
        }

        public static string GetAsCSVString (List<Flight> flights)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(GetHeaderString());
            foreach (Flight flight in flights)
            {
                sb.AppendLine(flight.ToString());
            }
            return sb.ToString();
        }

        public Flight() { }
        public Flight(AvailabilityDataModel availability, SeatType seatType)
        {
            Route = new Route(availability.Route);
            ID = availability.ID;
            Date = availability.ParsedDate;
            AvailabilityTrips = availability.AvailabilityTrips;
            SeatType = seatType;

            MileageProgram thisSource = MileageProgram.None;
            Enum.TryParse(availability.Source, true, out thisSource);
            Source = thisSource;

            switch (seatType)
            {
                case SeatType.Economy:
                    SetClassInfo(availability.YAvailable, availability.YMileageCost, availability.YRemainingSeats,
                        availability.YAirlines, availability.YDirect);
                    break;
                case SeatType.PremiumEconomy:
                    SetClassInfo(availability.WAvailable, availability.WMileageCost, availability.WRemainingSeats,
                        availability.WAirlines, availability.WDirect);
                    break;
                case SeatType.Business:
                    SetClassInfo(availability.JAvailable, availability.JMileageCost, availability.JRemainingSeats,
                        availability.JAirlines, availability.JDirect);
                    break;
                case SeatType.First:
                    SetClassInfo(availability.FAvailable, availability.FMileageCost, availability.FRemainingSeats,
                        availability.FAirlines, availability.FDirect);
                    break;
                default:
                    break;
            }
        }
        private void SetClassInfo(bool? available, string mileageCostString, int? remainingSeats, string airlines, bool? direct)
        {
            Available = available ?? false;
            int mileageCost = 0;
            if (int.TryParse(mileageCostString, out mileageCost))
            {
                MileageCost = mileageCost;
            }
            RemainingSeats = remainingSeats ?? 0;
            Airlines = airlines;
            Direct = direct ?? false;
        }

        public static List<Flight> GetFlights(FilterAggregate filterAggregate, AvailabilityDataModel availability)
        {
            List<Flight> results = new List<Flight>();
            results.Add(new Flight(availability, SeatType.Economy));
            results.Add(new Flight(availability, SeatType.PremiumEconomy));
            results.Add(new Flight(availability, SeatType.Business));
            results.Add(new Flight(availability, SeatType.First));
            return filterAggregate.Filter(results);
        }

        public static List<Flight> GetFilteredFlights(FilterAggregate filterAggregate, List<AvailabilityDataModel>? availableData)
        {
            if (availableData == null)
            {
                return new List<Flight>();
            }

            List<Flight> flights = new List<Flight>();
            foreach (var availability in availableData)
            {
                flights.AddRange(Flight.GetFlights(filterAggregate, availability));
            }
            return flights;
        }

        public bool Equals(Route? other)
        {
            if (other is null) return false;
            if (Route.Origin.Equals(other.Origin) == false) return false;
            if (Route.Destination.Equals(other.Destination) == false) return false;
            return true;
        }

        public bool Equals(Flight? other)
        {
            if (other == null) return false;
            if (other.Route.Equals(this.Route) == false) return false;
            if (other.SeatType.Equals(this.SeatType) == false) return false;
            if (other.Date.Equals(this.Date) == false) return false;
            if (other.Source.Equals(this.Source) == false) return false;
            if (other.Available.Equals(this.Available) == false) return false;
            if (other.RemainingSeats.Equals(this.RemainingSeats) == false) return false;
            if (other.Airlines.Equals(this.Airlines) == false) return false;
            if (other.Direct.Equals(this.Direct) == false) return false;
            if (other.MileageCost.Equals(this.MileageCost) == false) return false;
            return true;
        }

        public int CompareTo(Flight? other)
        {
            int i = 0;
            if (other == null) return 1;
            if (MiscHelper.IfNotComparable(this, other, (Flight x) => x.Route.Origin.AirportCode.ToString() , out i) == false) return i;
            if (MiscHelper.IfNotComparable(this, other, (Flight x) => x.Route.Destination.AirportCode.ToString(), out i) == false) return i;
            if (MiscHelper.IfNotComparable((int)other.SeatType,(int)this.SeatType, out i) == false) return i;
            if (MiscHelper.IfNotComparable(other.Direct, this.Direct, out i) == false) return i;
            if (MiscHelper.IfNotComparable(this.MileageCost, other.MileageCost, out i) == false) return i;
            if (MiscHelper.IfNotComparable(other.RemainingSeats, this.RemainingSeats, out i) == false) return i;
            if (MiscHelper.IfNotComparable(this.Date, other.Date, out i) == false) return i;
            if (MiscHelper.IfNotComparable((int)this.Source, (int)other.Source, out i) == false) return i;
            return 0;
        }

    }
}
