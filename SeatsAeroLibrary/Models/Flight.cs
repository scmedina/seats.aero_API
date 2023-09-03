using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models
{
    public class Flight
    {
        public string Id { get; set; }
        public Route Route { get; set; }
        public DateTime ParsedDate { get; set; }

        public ClassAvailability YAvailability { get; set; }
        public ClassAvailability WAvailability { get; set; }
        public ClassAvailability JAvailability { get; set; }
        public ClassAvailability FAvailability { get; set; }

        public ClassAvailability GetClassAvailability(SeatType seatType)
        {
            switch (seatType)
            {
                case SeatType.Y:
                    return YAvailability;
                case SeatType.W:
                    return WAvailability;
                case SeatType.J:
                    return JAvailability;
                default:
                    return FAvailability;
            }
        }

        public MileageProgram Source { get; set; }
        public string ComputedLastSeen { get; set; }
        public object AvailabilityTrips { get; set; }


        public override string ToString()
        {
            return $"ID: {Id}, Date: {ParsedDate}, YAvailability: {YAvailability}, WAvailability: {WAvailability}," +
                $" JAvailability: {JAvailability}, FAvailability: {FAvailability}, Source: {Source}";
        }

        public Flight(AvailabilityDataModel availability)
        {
            Id = availability.Id ;
            Route = availability.Route ;
            ParsedDate = availability.ParsedDate ;
            ComputedLastSeen = availability.ComputedLastSeen ;
            AvailabilityTrips = availability.AvailabilityTrips ;

            MileageProgram thisSource = MileageProgram.None;
            if (Enum.TryParse(availability.Source, true, out thisSource) == true)
            {
                Source = thisSource;
            }

            YAvailability = new ClassAvailability(
                availability.YAvailable, availability.YMileageCost, availability.YRemainingSeats, 
                availability.YAirlines, availability.YDirect);
            WAvailability = new ClassAvailability(
                availability.WAvailable, availability.WMileageCost, availability.WRemainingSeats,
                availability.WAirlines, availability.WDirect);
            JAvailability = new ClassAvailability(
                availability.JAvailable, availability.JMileageCost, availability.JRemainingSeats,
                availability.JAirlines, availability.JDirect);
            FAvailability = new ClassAvailability(
                availability.FAvailable, availability.FMileageCost, availability.FRemainingSeats,
                availability.FAirlines, availability.FDirect);
        }
    }
}
