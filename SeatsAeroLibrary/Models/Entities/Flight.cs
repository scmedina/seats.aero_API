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
    public class Flight
    {
        public Route Route { get; set; }
        public DateTime Date { get; set; }
        private string ComputedLastSeen { get; set; }
        private object AvailabilityTrips { get; set; }
        public MileageProgram Source { get; set; }
        public string SourceString
        {
            get { return Source.ToString(); }
        }
        public bool Available { get; set; }
        public int RemainingSeats { get; set; }
        public string Airlines { get; set; }
        public bool Direct { get; set; }
        public int MileageCost { get; set; }

        public override string ToString()
        {
            return $"Available: {Available}, RemainingSeats: {RemainingSeats}, Direct: {Direct}";
        }

        public Flight(AvailabilityDataModel availability, SeatType seatType)
        {
            Route = Route.GetRoute(availability.Route);
            Date = availability.ParsedDate;
            ComputedLastSeen = availability.ComputedLastSeen;
            AvailabilityTrips = availability.AvailabilityTrips;

            MileageProgram thisSource = MileageProgram.None;
            Enum.TryParse(availability.Source, true, out thisSource);
            Source = thisSource;

            switch (seatType)
            {
                case SeatType.YEconomy:
                    SetClassInfo(availability.YAvailable, availability.YMileageCost, availability.YRemainingSeats,
                        availability.YAirlines, availability.YDirect);
                    break;
                case SeatType.WPremiumEconomy:
                    SetClassInfo(availability.WAvailable, availability.WMileageCost, availability.WRemainingSeats,
                        availability.WAirlines, availability.WDirect);
                    break;
                case SeatType.JBusiness:
                    SetClassInfo(availability.JAvailable, availability.JMileageCost, availability.JRemainingSeats,
                        availability.JAirlines, availability.JDirect);
                    break;
                case SeatType.FFirstClass:
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

        public static List<Flight> GetClassAvailabilities(AvailabilityDataModel availability)
        {
            List<Flight> results = new List<Flight>();
            results.Add(new Flight(availability, SeatType.YEconomy));
            results.Add(new Flight(availability, SeatType.WPremiumEconomy));
            results.Add(new Flight(availability, SeatType.JBusiness));
            results.Add(new Flight(availability, SeatType.FFirstClass));
            return results;
        }
    }
}
