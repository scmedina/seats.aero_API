
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
        protected string _filePath = "";
        protected IConfigSettings _configSettings { get; set; }

        public StatisticsRepository()
        {
            using (var scope = ServicesContainer.BuildContainer().BeginLifetimeScope())
            {
                _configSettings = scope.Resolve<IConfigSettings>();
            }
            _configSettings.Load();
            _filePath = $@"{_configSettings.OutputDirectory}\\Statistics_{DateTime.Now:yyyyMMdd}_{DateTime.Now:HHmmss}.json";
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

            ExportStatistics();
        }


        protected void ExportStatistics()
        {
            FileIO.ExportJsonFile(this, _filePath);
        }

    }
}
