using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.Entities
{
    public class SearchCriteria
    {
        public FilterAggregate FilterAggregate { get; set; }
        public string OriginAirports { get; set; }
        public string DestinationAirports { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Direct { get; set; }
        public string SeatTypesString { get; set; }
        public int MaxMileage { get; set; }
        public int MinimumSeats { get; set; }
        public bool Exclude { get; set; }
        public SeatType SeatTypeEnum { get; set; }
        public List<SeatType> SeatTypesList { get; set; }
        public string Sources { get; set; }

        public SearchCriteria(SearchCriteriaDataModel searchCriteriaDataModel) 
        {
            this.OriginAirports = searchCriteriaDataModel.OriginAirports ?? "";
            this.DestinationAirports = searchCriteriaDataModel.DestinationAirports ?? "";
            this.StartDate = searchCriteriaDataModel.StartDate;
            this.EndDate = searchCriteriaDataModel.EndDate;
            this.Direct = searchCriteriaDataModel.Direct ?? false;
            this.SeatTypesString = searchCriteriaDataModel.SeatTypes;
            this.MaxMileage = searchCriteriaDataModel.MaxMileage ?? 0;
            this.MinimumSeats = searchCriteriaDataModel.MinimumSeats ?? 0;
            this.Exclude = searchCriteriaDataModel.Exclude ?? false;
            this.Sources = searchCriteriaDataModel.Sources ?? "";

            string[] seatTypesArray = searchCriteriaDataModel.SeatTypes.Split(',');
            SeatType seatTypesEnum = SeatType.None;
            foreach (string seatTypeString in seatTypesArray)
            {
                SeatType thisSeatTypeEnum = SeatType.None;
                if (Enum.TryParse(seatTypeString.Trim(), out thisSeatTypeEnum))
                {
                    seatTypesEnum = (seatTypesEnum | thisSeatTypeEnum);
                }
            }

            EnumHelper enumHelper = new EnumHelper();
            SeatTypesList = enumHelper.GetBitFlagList(seatTypesEnum);
        }

    }
}
