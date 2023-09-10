using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models
{
    public class ClassAvailability
    {

        [JsonPropertyName("available")]
        public bool Available { get; set; }

        [JsonPropertyName("mileageCostString")]
        public string MileageCostString { get; set; }

        [JsonPropertyName("remainingSeats")]
        public int RemainingSeats { get; set; }

        [JsonPropertyName("airlines")]
        public string Airlines { get; set; }

        [JsonPropertyName("direct")]
        public bool Direct { get; set; }

        [JsonPropertyName("mileageCost")]
        public int MileageCost
        {
            get
            {
                int value = 0;
                if (int.TryParse(MileageCostString, out value) == true)
                {
                    return value;
                }
                else
                {
                    return 0;
                }
            }
        }

        public override string ToString()
        {
            return $"Available: {Available}, RemainingSeats: {RemainingSeats}, Direct: {Direct}";
        }

        public ClassAvailability(bool? available, string mileageCostString, int? remainingSeats, string airlines, bool? direct)
        {
            Available = available?? false;
            MileageCostString = mileageCostString;
            RemainingSeats = remainingSeats?? 0;
            Airlines = airlines;
            Direct = direct?? false;
        }
    }
}
