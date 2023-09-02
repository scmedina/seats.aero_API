using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models
{
    public class FilterAggregate
    {
        //public SeatType seatTypes = SeatType.Any;

        public bool IsDirect = true;
        public int MinRemainingSeats = 4;
        public FilterAggregate() 
        { 
        }

        public List<AvailabilityDataModel> Filter(List<AvailabilityDataModel> availabilityList)
        {
            List<AvailabilityDataModel> results = new List<AvailabilityDataModel>();

            results = availabilityList.Where(value => value.JAvailable = true).ToList();
            results = results.Where(value => value.JRemainingSeats>=MinRemainingSeats).ToList();

            return results;
        }
    }
}
