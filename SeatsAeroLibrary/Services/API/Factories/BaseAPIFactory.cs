using SeatsAeroLibrary.Repositories;
using SeatsAeroLibrary.Services.API.APICall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.API.Factories
{
    public class BaseAPIFactory: IAPIFactory
    {
        protected IConfigSettings _configSettings { get; set; }
        protected IStatisticsRepository _statisticsRepository { get; set; }
        protected ILogger _logger { get; set; }

        public BaseAPIFactory(IConfigSettings configSettings, IStatisticsRepository statisticsRepository, ILogger logger)
        {
            _configSettings = configSettings;
            _statisticsRepository = statisticsRepository;
            _logger = logger;
        }

        public TApi CreateAPI<TApi>() where TApi : IAPI, new()
        {
            TApi result = new TApi();
            result.Initialize(_configSettings, _statisticsRepository, _logger);
            return result;
        }

    }
}
