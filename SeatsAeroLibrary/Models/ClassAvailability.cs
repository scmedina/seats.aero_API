using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models
{
    public class ClassAvailability
    {
        public bool Available { get; set; }
        public string MileageCostString { get; set; }
        public int RemainingSeats { get; set; }
        public string Airlines { get; set; }
        public bool Direct { get; set; }

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
            return $"Available: {Available}, MileageCostString: {MileageCostString}, RemainingSeats: {RemainingSeats}, Airlines: {Airlines}," +
                $" Direct: {Direct}";
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
