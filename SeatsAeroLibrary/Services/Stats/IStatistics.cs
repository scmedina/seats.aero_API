using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.Stats
{
    public interface IStatistics
    {
        Dictionary<string, int> APICallsCount { get; set; }
        int TotalAPICalls { get; set; }
        public void SetCurrentAPICall(string searchName);
        public void IncrementAPICall();
    }
}
