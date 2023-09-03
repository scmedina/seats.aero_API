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
        public FilterAggregate(List<IFlightFilterFactory> filterFactories) 
        { 
            this.filters = new List<IFlightFilter>();

            if (filterFactories is null)
            {
                return;
            }

            foreach (IFlightFilterFactory factory in filterFactories)
            {
                this.filters.Add(factory.CreateFilter());
            }
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
