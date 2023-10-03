
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.Stats
{
    public class Statistics : Services.Stats.IStatistics
    {
        public Dictionary<string, int> APICallsCount { get; set; } = new Dictionary<string, int>();
        public int TotalAPICalls { get; set; }
        private string _currentKey = "";

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

    }
}
