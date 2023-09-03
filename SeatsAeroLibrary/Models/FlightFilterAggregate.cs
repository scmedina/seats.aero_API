using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models
{
    public class FilterAggregate
    {
        private List<IFlightFilter> filters;
        public FilterAggregate(List<IFlightFilter> filters) 
        { 
            this.filters = filters;
        }

        public List<Flight> Filter(List<Flight> flights)
        {
            List<Flight> results = new List<Flight>(flights);

            foreach (IFlightFilter filter in filters)
            {
                results = filter.Filter(results);
            }

            return results;
        }
    }
}
