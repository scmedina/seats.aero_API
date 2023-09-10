using SeatsAeroLibrary.Models.Entities;
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
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("route")]
        public Route Route { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("computedLastSeen")]
        public string ComputedLastSeen { get; set; }

        [JsonPropertyName("availabilityTrips")]
        public object AvailabilityTrips { get; set; }
        public MileageProgram Source { get; set; }

        [JsonPropertyName("sourceName")]
        public string SourceString
        {
            get { return Source.ToString(); }
        }

        [JsonPropertyName("economyAvailability")]
        public ClassAvailability EconomyAvailability { get; set; }

        [JsonPropertyName("premiumEconomyAvailability")]
        public ClassAvailability PremiumEconomyAvailability { get; set; }

        [JsonPropertyName("businessAvailability")]
        public ClassAvailability BusinessAvailability { get; set; }

        [JsonPropertyName("firstAvailability")]
        public ClassAvailability FirstAvailability { get; set; }

        public ClassAvailability GetClassAvailability(SeatType seatType)
        {
            switch (seatType)
            {
                case SeatType.YEconomy:
                    return EconomyAvailability;
                case SeatType.WPremiumEconomy:
                    return PremiumEconomyAvailability;
                case SeatType.JBusiness:
                    return BusinessAvailability;
                case SeatType.FFirstClass: 
                    return FirstAvailability;
                default:
                    return EconomyAvailability;
            }
        }



        public override string ToString()
        {
            return $"Date: {Date.Date:d}, Route: [{Route}], {nameof(EconomyAvailability)}: [{EconomyAvailability}], {nameof(BusinessAvailability)}: [{BusinessAvailability}]," +
                $"{nameof(FirstAvailability)}: [{FirstAvailability}, Source: {Source}], {nameof(PremiumEconomyAvailability)}: [{PremiumEconomyAvailability}]";
        }

        public Flight(AvailabilityDataModel availability)
        {
            Id = availability.Id ;
            Route = Route.GetRoute(availability.Route) ;
            Date = availability.ParsedDate ;
            ComputedLastSeen = availability.ComputedLastSeen ;
            AvailabilityTrips = availability.AvailabilityTrips ;

            MileageProgram thisSource = MileageProgram.None;
            Enum.TryParse(availability.Source, true, out thisSource);
            Source = thisSource;

            EconomyAvailability = new ClassAvailability(
                availability.YAvailable, availability.YMileageCost, availability.YRemainingSeats, 
                availability.YAirlines, availability.YDirect);
            PremiumEconomyAvailability = new ClassAvailability(
                availability.WAvailable, availability.WMileageCost, availability.WRemainingSeats,
                availability.WAirlines, availability.WDirect);
            BusinessAvailability = new ClassAvailability(
                availability.JAvailable, availability.JMileageCost, availability.JRemainingSeats,
                availability.JAirlines, availability.JDirect);
            FirstAvailability = new ClassAvailability(
                availability.FAvailable, availability.FMileageCost, availability.FRemainingSeats,
                availability.FAirlines, availability.FDirect);
        }
    }
}
