using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Models.Entities;
using SeatsAeroLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services
{
    public class TripSearchService : ITripSearchService
    {
        private IConfigSettings _configSettings;
        private IFlightRecordService _flightRecordService;
        private IStatisticsRepository _statisticsRepository;
        private ISearchCriteriaService _searchCriteriaService;
        private ILogger _logger;

        public TripSearchService(IConfigSettings configSettings, IFlightRecordService flightRecordService, IStatisticsRepository statisticsRepository, ILogger logger, ISearchCriteriaService searchCriteriaService)
        {
            _configSettings = configSettings;
            _flightRecordService = flightRecordService;
            _statisticsRepository = statisticsRepository;
            _logger = logger;
            _searchCriteriaService = searchCriteriaService;
        }

        public List<TripSearch> GetTripSearches(IEnumerable<TripSearchDataModel> searches, IFilterAnalyzer filterAnalyzer = null)
        {
            List<TripSearch> result = new List<TripSearch>();
            foreach (TripSearchDataModel search in searches)
            {
                TripSearch tripSearch = new TripSearch(_configSettings, _flightRecordService, _statisticsRepository, _logger);
                tripSearch.GetTripSearch(_searchCriteriaService, search, filterAnalyzer);
                result.Add(tripSearch);
            }
            return result;
        }

        public void GetAllFlightsFromCachedSearches(List<TripSearch> trips)
        {
            foreach (TripSearch trip in trips)
            {
                trip.GetAllFlightsFromCachedSearch();
            }
        }
    }
}
