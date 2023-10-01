using Autofac;
using SeatsAeroLibrary.API;
using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Services;
using SeatsAeroLibrary.Services.FlightFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.Entities
{
    public class SearchCriteria
    {
        private static ILogger _logger;
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

        public override string ToString()
        {
            return $"{OriginAirports} > {DestinationAirports} ({StartDate} - {EndDate})";
        }

        public SearchCriteria()
        {
            using (var scope = ServicesContainer.BuildContainer().BeginLifetimeScope())
            {
                _logger = scope.Resolve<ILogger>();
            }
        }

        public SearchCriteria(SearchCriteriaDataModel searchCriteriaDataModel, IFilterAnalyzer filterAnalyzer = null) : this()
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
            SeatTypeEnum = SeatType.None;
            foreach (string seatTypeString in seatTypesArray)
            {
                SeatType thisSeatTypeEnum = SeatType.None;
                Guard.AgainstInvalidSeatType(seatTypeString, nameof(seatTypeString), out thisSeatTypeEnum);
                SeatTypeEnum = (SeatTypeEnum | thisSeatTypeEnum);
            }

            EnumHelper enumHelper = new EnumHelper();
            SeatTypesList = enumHelper.GetBitFlagList(SeatTypeEnum);

            FilterAggregate = BuildFilter(filterAnalyzer);
        }

        protected virtual FilterAggregate BuildFilter(IFilterAnalyzer filterAnalyzer = null)
        {
            List<IFlightFilter> filters = new List<IFlightFilter>();
            filters.AddRange(FlightFilterFactoryHelpers.GetFilters<LocationFilterFactory>(this));
            filters.AddRange(FlightFilterFactoryHelpers.GetFilters<DateFilterFactory>(this));
            filters.AddRange(FlightFilterFactoryHelpers.GetFilters<DirectFilterFactory>(this));
            filters.AddRange(FlightFilterFactoryHelpers.GetFilters<SeatTypeFilterFactory>(this));
            filters.AddRange(FlightFilterFactoryHelpers.GetFilters<MaxMileageCostFilterFactory>(this));
            filters.AddRange(FlightFilterFactoryHelpers.GetFilters<SeatAvailabilityFilterFactory>(this));

            FilterAggregate filterAggregate = new FilterAggregate(filters, filterAnalyzer);
            return filterAggregate;
        }

        public static List<SearchCriteria> GetSearchCriteria(List<SearchCriteriaDataModel> searchCriteria, IFilterAnalyzer filterAnalyzer = null)
        {
            List<SearchCriteria> results = new List<SearchCriteria>();
            foreach (SearchCriteriaDataModel searchCriteriaDataModel in searchCriteria)
            {
                results.Add(new SearchCriteria(searchCriteriaDataModel, filterAnalyzer));
            }
            return results;
        }

        public async Task<List<Flight>> GetFlightsFromCachedSearch()
        {
            SeatsAeroCacheSearchAPI apiCall = new SeatsAeroCacheSearchAPI(this.FilterAggregate);

            _logger.Info($"Querying Availability API Result: {apiCall.OriginAirports} > {apiCall.DestinationAirports}");

            List<Flight> results = await apiCall.QueryResults();

            _logger.Info($"Availability API Result Completed: {apiCall.OriginAirports} > {apiCall.DestinationAirports}");

            return results;
        }

        internal IEnumerable<Flight> GetFlightsFromCachedSearchSync()
        {
            Task<List<Flight>> task = GetFlightsFromCachedSearch();
            task.Wait();
            return task.Result; 
        }
    }
}
