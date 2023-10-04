using Autofac;
using SeatsAeroLibrary.API;
using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Repositories;
using SeatsAeroLibrary.Services;
using SeatsAeroLibrary.Services.API.Factories;
using SeatsAeroLibrary.Services.FlightFactories;
using SeatsAeroLibrary.Services.Sort;
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
        public string Sort { get; set; }
        public string SortDirection { get; set; }

        protected ILogger _logger { get; set; }

        protected IAPIWithFiltersFactory _aPIWithFiltersFactory;

        public override string ToString()
        {
            return $"{OriginAirports} > {DestinationAirports} ({StartDate} - {EndDate})";
        }

        public SearchCriteria(ILogger logger, IAPIWithFiltersFactory aPIWithFiltersFactory)
        {
            _logger = logger;
            _aPIWithFiltersFactory = aPIWithFiltersFactory;
            _logger.Info("SearchCriteria constructor");
        }

        public SearchCriteria( ILogger logger,SearchCriteriaDataModel searchCriteriaDataModel, IAPIWithFiltersFactory aPIWithFiltersFactory, IFilterAnalyzer filterAnalyzer = null) : this(logger, aPIWithFiltersFactory)
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
            this.Sort = searchCriteriaDataModel.Sort ?? "";
            this.SortDirection = searchCriteriaDataModel.SortDirection ?? "";

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
            filters.AddRange(FlightFilterFactoryHelpers.GetFilters<SourceFilterFactory>(this));

            FilterAggregate filterAggregate = new FilterAggregate(filters, filterAnalyzer);
            return filterAggregate;
        }

        public async Task<List<Flight>> GetFlightsFromCachedSearch()
        {
            SeatsAeroCacheSearchAPI apiCall = _aPIWithFiltersFactory.CreateAPI<SeatsAeroCacheSearchAPI>(this.FilterAggregate);

            if (Exclude)
            {
                _logger.Info($"Following search criteria is flagged as exclude: {apiCall.OriginAirports} > {apiCall.DestinationAirports}");
                return new List<Flight>();
            }

            _logger.Info($"Querying Availability API Result: {apiCall.OriginAirports} > {apiCall.DestinationAirports}");

            List<Flight> results = await apiCall.QueryResults();
            results = results.OrderBy(flight => flight.MileageCost).ToList();

            _logger.Info($"Availability API Result Completed: {apiCall.OriginAirports} > {apiCall.DestinationAirports}");


            results = (List<Flight>)BasicSorter<Flight>.SortTs(results, Sort, SortDirection).ToList();

            return results;
        }

        public IEnumerable<Flight> GetFlightsFromCachedSearchSync()
        {
            Task<List<Flight>> task = GetFlightsFromCachedSearch();
            task.Wait();
            return task.Result; 
        }
    }
}
