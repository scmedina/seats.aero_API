using SeatsAeroLibrary.API;
using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Repositories;
using SeatsAeroLibrary.Services.API.APICall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.API.Factories
{
    public class APIWithFiltersFactory : BaseAPIFactory, IAPIWithFiltersFactory
    {
        public APIWithFiltersFactory(IConfigSettings configSettings, IStatisticsRepository statisticsRepository, ILogger logger) : base(configSettings, statisticsRepository, logger)
        {
        }

        public TApi CreateAPI<TApi>(FilterAggregate filterAggregate) where TApi : IAPIWithFilters, new()
        {
            TApi result = base.CreateAPI<TApi>();
            result.SetFilterAggregate(filterAggregate);
            return result;
        }
    }
}
