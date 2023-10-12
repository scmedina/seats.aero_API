using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.DataModels
{
    public class SearchCriteriaDataModel
    {
        public string? OriginAirports { get; set; }
        public string? DestinationAirports { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? Direct { get; set; }
        public string? SeatTypes { get; set; }
        public int? MaxMileage { get; set; }
        public int? MinimumSeats { get; set; }
        public bool? Exclude { get; set; }
        public string Sort { get; set; }
        public string SortDirection { get; set; }
        public string Sources { get; set; }
    }
}
