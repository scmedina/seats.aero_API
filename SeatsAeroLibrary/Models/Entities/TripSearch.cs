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

        public TripSearch(IConfigSettings configSettings, IFlightRecordService flightRecordService, IStatisticsRepository statisticsRepository, ILogger logger)
        {
            _configSettings = configSettings;
            _flightRecordService = flightRecordService;
            _statisticsRepository = statisticsRepository;
            _logger = logger;
            _logger.Info("TripSearch constructor");
            _configSettings.Load();
        }


        public void GetTripSearch(ISearchCriteriaService searchCriteriaService, TripSearchDataModel search, IFilterAnalyzer filterAnalyzer = null)
        {
            ID = search.ID;
            Name = search.Name;
            Contact = search.Contact;
            FilterAnalyzer = filterAnalyzer;
            Exclude = search.Exclude ?? false;
            Sort = search.Sort;
            SortDirection = search.SortDirection;
            if (Exclude == false)
            {
                SearchCriteria = searchCriteriaService.GetSearchCriteria(search.SearchCriteria, filterAnalyzer);
            }
            else
            {
                SearchCriteria = new List<SearchCriteria>();
            }
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

    }
}
