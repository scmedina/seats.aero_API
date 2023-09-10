using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.Entities
{
    public class Route
    {
        protected static List<Route> Routes { get; set; } = new List<Route>();
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("origin")]
        public Location Origin { get; set; }
        [JsonPropertyName("destination")]
        public Location Destination { get; set; }
        [JsonPropertyName("distance")]
        public int Distance { get; set; }
        public MileageProgram Source { get; set; }

        public Route(RouteDataModel routeDataModel) 
        { 
            Id = routeDataModel.Id;
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

        public static Route GetRoute(RouteDataModel routeDataModel)
        {
            Route result = Routes.FirstOrDefault(route => route.Id == routeDataModel.Id);
            if (result == null)
            {
                result = new Route(routeDataModel);
            }
            else
            {
                return result;
            }
            return result;
        }
    }
}
