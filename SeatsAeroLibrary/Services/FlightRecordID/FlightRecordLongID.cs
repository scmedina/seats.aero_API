﻿using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.FlightRecordID
{

    public class FlightRecordLongID :  IFlightRecordID
    {
        public string OriginAirport { get; set; }
        public string DestinationAirport { get; set; }
        public string SeatType { get; set; }
        public string DayOfWeek { get; set; }
        public bool Direct { get; set; }
        public string Airline { get; set; }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + (OriginAirport?.GetHashCode() ?? 0);
            hash = hash * 23 + (DestinationAirport?.GetHashCode() ?? 0);
            hash = hash * 23 + (SeatType?.GetHashCode() ?? 0);
            hash = hash * 23 + (DayOfWeek?.GetHashCode() ?? 0);
            hash = hash * 23 + Direct.GetHashCode();
            hash = hash * 23 + (Airline?.GetHashCode() ?? 0);
            return hash;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            FlightRecordLongID other = (FlightRecordLongID)obj;
            return OriginAirport == other.OriginAirport &&
                DestinationAirport == other.DestinationAirport &&
                SeatType == other.SeatType &&
                DayOfWeek == other.DayOfWeek &&
                Direct == other.Direct &&
                Airline == other.Airline;
        }

        public int CompareToID(FlightRecordLongID? other)
        {
            if (other == null)
            {
                return 1; // null is considered greater
            }
            else if (OriginAirport.CompareTo(other.OriginAirport) != 0)
            {
                return OriginAirport.CompareTo(other.OriginAirport);
            }
            else if (DestinationAirport.CompareTo(other.DestinationAirport) != 0)
            {
                return DestinationAirport.CompareTo(other.DestinationAirport);
            }
            else if (Direct.CompareTo(other.Direct) != 0)
            {
                return Direct.CompareTo(other.Direct);
            }
            else if (SeatType.CompareTo(other.SeatType) != 0)
            {
                return SeatType.CompareTo(other.SeatType);
            }
            else if (Airline.CompareTo(other.Airline) != 0)
            {
                return Airline.CompareTo(other.Airline);
            }
            else
            {
                return 0;
            }

        }

        public int CompareTo(object? obj)
        {
            FlightRecordLongID id = obj as FlightRecordLongID;
            if (id != null)
            {
                return CompareToID(id);
            }
            else
            {
                throw new ArgumentException("Object is not a FlightRecordLongID");
            }
        }

        public void Map(FlightRecordDataModel flightRecordDataModel)
        {
            GenericMapper.Map(flightRecordDataModel, this);
        }
    }
}