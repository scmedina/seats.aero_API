using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.FlightFilters
{
    public class FlightFiltersHelpers
    {


        public static bool GetFilters<T>(List<IFlightFilter> filters, ref List<T> results, Func<T, bool> predicate) where T: IFlightFilter
        {
            results = null;
            if (filters == null || filters.Count == 0)
            {
                return false;
            }

            results = filters.OfType<T>().Where(predicate).ToList();
            
            return results != null && results.Count != 0;
        }

    }
}
