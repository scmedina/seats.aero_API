
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


        public void AddAPICall(string searchName)
        {
            APICallsCount.Add(searchName, 0);
        }
        public void IncrementAPICall()
        {
            // Get the last key-value pair using LINQ
            var lastEntry = APICallsCount.Last();

            // Increment the value associated with the last key
            APICallsCount[lastEntry.Key]++;

            TotalAPICalls++;
        }

    }
}
