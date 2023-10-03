using Autofac;
using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.Stats
{
    public class StatisticsHelper
    {

        protected IConfigSettings _configSettings { get; set; }
        protected IStatistics _statistics { get; set; }
        public StatisticsHelper()
        {
            using (var scope = ServicesContainer.BuildContainer().BeginLifetimeScope())
            {
                _configSettings = scope.Resolve<IConfigSettings>();
                _statistics = scope.Resolve<IStatistics>();
            }
            _configSettings.Load();
        }

        public static IStatistics GetStatistics()
        {
            IStatistics statistics = null;
            using (var scope = ServicesContainer.BuildContainer().BeginLifetimeScope())
            {
                statistics = scope.Resolve<IStatistics>();
            }
            return statistics;
        }

        public static void ExportStatistics()
        {
            IConfigSettings configSettings = null;
            IStatistics statistics = null;
            using (var scope = ServicesContainer.BuildContainer().BeginLifetimeScope())
            {
                configSettings = scope.Resolve<IConfigSettings>();
                statistics = scope.Resolve<IStatistics>();
            }
            configSettings.Load();
            string filePath = $@"{configSettings.OutputDirectory}\\Statistics_{DateTime.Now:yyyyMMdd}_{DateTime.Now:HHmmss}.json";
            FileIO.ExportJsonFile(statistics, filePath);
        }
    }
}
