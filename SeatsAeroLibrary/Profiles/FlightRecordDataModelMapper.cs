using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Profiles
{
    public class FlightRecordDataModelMapper : BasicMapper<FlightRecord, FlightRecordDataModel>
    {
        public override FlightRecordDataModel Map(FlightRecord source)
        {
            var record = new FlightRecordDataModel
            {
                Id = source.Id,
                AvailabilityID = source.AvailabilityID,
                OriginAirport = source.OriginAirport,
                OriginRegion = source.OriginRegion,
                DestinationAirport = source.DestinationAirport,
                DestinationRegion = source.DestinationRegion,
                SeatType = source.SeatType.ToString(),
                DayOfWeek = source.DayOfWeek.ToString(),
                Direct = source.Direct,
                MileageCost = source.MileageCost,
                Date = source.Date,
                Airline = source.Airline,
                Source = source.Source.ToString()
            };
            return record;
        }

        public override FlightRecord Map(FlightRecordDataModel source)
        {
            var record = new FlightRecord
            {
                Id = source.Id,
                AvailabilityID = source.AvailabilityID,
                OriginAirport = source.OriginAirport,
                OriginRegion = source.OriginRegion,
                DestinationAirport = source.DestinationAirport,
                DestinationRegion = source.DestinationRegion,
                SeatType = (SeatType)Enum.Parse(typeof(SeatType), source.SeatType),
                DayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), source.DayOfWeek),
                Direct = source.Direct,
                MileageCost = source.MileageCost,
                Date = source.Date,
                Airline = source.Airline,
                Source = (MileageProgram)Enum.Parse(typeof(MileageProgram), source.Source)
            };
            return record;
        }
    }
}
