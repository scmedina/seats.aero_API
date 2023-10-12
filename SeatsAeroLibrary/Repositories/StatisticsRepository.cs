
using Autofac;
using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        public Dictionary<string, int> APICallsCount { get; set; } = new Dictionary<string, int>();
        public int TotalAPICalls { get; set; }
        private string _currentKey = "";
        protected ILogger _logger { get; set; }
        protected IConfigSettings _configSettings { get; set; }

        public StatisticsRepository(IConfigSettings configSettings, ILogger logger)
        {
            _configSettings = configSettings;
            _logger = logger;
            _configSettings.Load();
        }

        public void SetCurrentAPICall(string searchName)
        {
            _currentKey = searchName;
            if (!APICallsCount.ContainsKey(searchName))
            {
                APICallsCount.Add(searchName, 0);
            }
        }
        public void IncrementAPICall()
        {
            APICallsCount[_currentKey]++;

            TotalAPICalls++;
        }


        protected void ExportStatistics()
        {
            string json = FileIO.GetAsJsonString(this);
            _logger.Info(json);
        }

    }
}
