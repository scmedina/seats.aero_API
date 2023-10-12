using SeatsAeroLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.API.APICall
{
    public interface IAPI
    {
        public string[] RequiredParams { get; set; }
        public Dictionary<string, string> QueryParams { get; set; }
        public string EndPoint { get; set; }
        public void Initialize(IConfigSettings configSettings, IStatisticsRepository statisticsRepository, ILogger logger);
        public void SetParams(string endPoint, string[] requiredParams, Dictionary<string, string> queryParams);
    }
}
