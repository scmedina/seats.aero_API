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
    public class FlightRecordMapper : BasicMapper<Flight, FlightRecordDataModel>
    {
        public override FlightRecordDataModel Map(Flight source)
        {
            var record = new FlightRecordDataModel
            {
                AvailabilityID = source.ID,
                OriginAirport = source.Route.Origin.AirportCode,
                OriginRegion = source.Route.Origin.Region,
                DestinationAirport = source.Route.Destination.AirportCode,
                DestinationRegion = source.Route.Destination.Region,
                SeatType = source.SeatType.ToString(),
                Date = source.Date,
                DayOfWeek = source.Date.DayOfWeek.ToString(),
                Direct = source.Direct,
                MileageCost = source.MileageCost,
                Airline = source.Airlines,
                Source = source.Source.ToString(),
                CreatedAt = source.CreatedAt,
                UpdatedAt = source.UpdatedAt
            };
            return record;
        }

        public override Flight Map(FlightRecordDataModel source)
        {
            var record = new Flight
            {
                ID = source.AvailabilityID,
                Route = new Route
                {
                    Origin = new Location(source.OriginAirport,source.OriginRegion),
                    Destination = new Location(source.DestinationAirport,source.DestinationRegion)
                },
                SeatType = (SeatType)Enum.Parse(typeof(SeatType), source.SeatType),
                Date = source.Date, 
                Direct = source.Direct, 
                MileageCost = source.MileageCost,
                Airlines = source.Airline,
                Source = (MileageProgram)Enum.Parse(typeof(MileageProgram), source.Source),
                CreatedAt = source.CreatedAt,
                UpdatedAt = source.UpdatedAt
            };
            return record;
        }
    }
}
