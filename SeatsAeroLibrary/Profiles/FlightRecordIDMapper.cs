using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Profiles
{
    public class FlightRecordIDMapper : BasicMapper<FlightRecordUniqueID, FlightRecordDataModel>
    {
        public override FlightRecordDataModel Map(FlightRecordUniqueID source)
        {
            var record = new FlightRecordDataModel
            {
                AvailabilityID = source.AvailabilityID,
                OriginAirport = source.OriginAirport,
                OriginRegion = source.OriginRegion,
                DestinationAirport = source.DestinationAirport,
                DestinationRegion = source.DestinationRegion,
                SeatType = source.SeatType,
                DayOfWeek = source.DayOfWeek,
                Direct = source.Direct,
                MileageCost = source.MileageCost,
                Date = source.Date,
                Airline = source.Airline,
                Source = source.Source
            };
            return record;
        }

        public override FlightRecordUniqueID Map(FlightRecordDataModel source)
        {
            var record = new FlightRecordUniqueID
            {
                AvailabilityID = source.AvailabilityID,
                OriginAirport = source.OriginAirport,
                OriginRegion = source.OriginRegion,
                DestinationAirport = source.DestinationAirport,
                DestinationRegion = source.DestinationRegion,
                SeatType = source.SeatType,
                DayOfWeek = source.DayOfWeek,
                Direct = source.Direct,
                MileageCost = source.MileageCost,
                Date = source.Date,
                Airline = source.Airline,
                Source = source.Source
            };
            return record;
        }
    }
}
