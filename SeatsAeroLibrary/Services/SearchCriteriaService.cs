using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Models.Entities;
using SeatsAeroLibrary.Repositories;
using SeatsAeroLibrary.Services.API.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services
{
    public class SearchCriteriaService : ISearchCriteriaService
    {

        private IConfigSettings _configSettings;
        private IStatisticsRepository _statisticsRepository;
        private ILogger _logger;
        private IAPIWithFiltersFactory _aPIWithFiltersFactory;

        public SearchCriteriaService(IConfigSettings configSettings, IStatisticsRepository statisticsRepository, ILogger logger, IAPIWithFiltersFactory aPIWithFiltersFactory)
        {
            _configSettings = configSettings;
            _statisticsRepository = statisticsRepository;
            _logger = logger;
            _aPIWithFiltersFactory = aPIWithFiltersFactory;
        }

        public List<SearchCriteria> GetSearchCriteria(List<SearchCriteriaDataModel> searchCriteria, IFilterAnalyzer filterAnalyzer = null)
        {
            List<SearchCriteria> results = new List<SearchCriteria>();
            foreach (SearchCriteriaDataModel searchCriteriaDataModel in searchCriteria)
            {
                results.Add(new SearchCriteria(_logger, searchCriteriaDataModel, _aPIWithFiltersFactory, filterAnalyzer));
            }
            return results;
        }
    }
}
