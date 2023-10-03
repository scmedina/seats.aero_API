using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SeatsAeroLibrary.API.Models;

namespace SeatsAeroLibrary.Models.Entities
{
    public class Route : IEquatable<RouteDataModel>
    {
        public Location Origin { get; set; }
        public Location Destination { get; set; }
        public int Distance { get; set; }
        public MileageProgram Source { get; set; }

        public Route() { }
        public Route(RouteDataModel routeDataModel) 
        { 
            Origin = new Location(routeDataModel.OriginAirport, routeDataModel.OriginRegion);
            Destination = new Location(routeDataModel.DestinationAirport, routeDataModel.DestinationRegion);
            Distance = routeDataModel.Distance;

            MileageProgram thisSource = MileageProgram.None;
            Enum.TryParse(routeDataModel.Source, true, out thisSource);
            Source = thisSource;
        }

        public override string ToString()
        {
            return $"{Origin} > {Destination}";
        }

        public bool Equals(RouteDataModel? other)
        {
            if (other is null) return false;
            if (Origin.AirportCode != other.OriginAirport) return false;
            if (Destination.AirportCode != other.DestinationAirport) return false;
            return true;
        }

        public static Dictionary<Route,List<Flight>> GetRoutes(List<Flight> flights)
        {
            Dictionary<Route,List<Flight>> results = new Dictionary<Route, List<Flight>>();

            // Group flights by Route
            var groupedByOrigin = flights.GroupBy(flight => flight.Route.ToString());

            // Iterate through the groups
            foreach (var group in groupedByOrigin)
            {
                List<Flight> groupFlights = new List<Flight>();
                foreach (var flight in group)
                {
                    groupFlights.Add(flight);
                }
                groupFlights.Sort();
                results.Add(groupFlights[0].Route, groupFlights);
            }
            return results;
        }

    }
}
