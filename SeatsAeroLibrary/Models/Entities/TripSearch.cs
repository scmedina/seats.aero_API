using Autofac;
using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Repositories;
using SeatsAeroLibrary.Services;
using SeatsAeroLibrary.Services.Sort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.Entities
{
    public class TripSearch
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public bool Exclude { get; set; }
        public string Sort { get; set; }
        public string SortDirection { get; set; }
        public List<SearchCriteria> SearchCriteria { get; set; } 
        public IFilterAnalyzer FilterAnalyzer { get; set; }

        protected IConfigSettings _configSettings { get; set; }
        protected IFlightRecordService _flightRecordService { get; set; }
        protected IStatisticsRepository _statisticsRepository { get; set; }
        protected ILogger _logger { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Contact})";
        }

        public TripSearch()
        {

            using (var scope = ServicesContainer.BuildContainer().BeginLifetimeScope())
            {
                _configSettings = scope.Resolve<IConfigSettings>();
                _flightRecordService = scope.Resolve<IFlightRecordService>();
                _statisticsRepository = scope.Resolve<IStatisticsRepository>();
                _logger = scope.Resolve<ILogger>();
            }
            _logger.Info("TripSearch constructor");
            _configSettings.Load();
        }

        public static List<TripSearch> GetTripSearches(IEnumerable<TripSearchDataModel> searches, IFilterAnalyzer filterAnalyzer = null)
        {
            List<TripSearch> result = new List<TripSearch>();
            foreach (TripSearchDataModel search in searches)
            {
                result.Add(GetTripSearch(search));
            }
            return result;
        }

        private static TripSearch GetTripSearch(TripSearchDataModel search, IFilterAnalyzer filterAnalyzer = null)
        {
            TripSearch result = new TripSearch();
            result.ID = search.ID;
            result.Name = search.Name;
            result.Contact = search.Contact;
            result.FilterAnalyzer = filterAnalyzer;
            result.Exclude = search.Exclude ?? false;
            result.Sort = search.Sort;
            result.SortDirection = search.SortDirection;
            if (result.Exclude == false)
            {
                result.SearchCriteria = Entities.SearchCriteria.GetSearchCriteria(search.SearchCriteria, filterAnalyzer);
            }
            else
            {
                result.SearchCriteria = new List<SearchCriteria>();
            }

            return result;
        }
        public void GetAllFlightsFromCachedSearch()
        {
            List<Flight> flights = new List<Flight>();
            foreach (SearchCriteria searchCriteria in this.SearchCriteria)
            {
                _statisticsRepository.SetCurrentAPICall(Name);
                flights.AddRange(searchCriteria.GetFlightsFromCachedSearchSync());
            }
            if (flights.Count == 0)
            {
                return;
            }
            flights = (List<Flight>)BasicSorter<Flight>.SortTs(flights, Sort, SortDirection).ToList();
            _flightRecordService.AddRecords(flights);
            string filePath = $@"{_configSettings.OutputDirectory}\\{this.Name}_{DateTime.Now:yyyyMMdd}_{DateTime.Now:HHmmss}";
            FileIO.SaveStringToFile(Flight.GetAsCSVString(flights), filePath + ".csv");
        }

        public static void GetAllFlightsFromCachedSearches(List<TripSearch> trips)
        {
            foreach (TripSearch trip in trips)
            {
                trip.GetAllFlightsFromCachedSearch();
            }

        }

    }
}
