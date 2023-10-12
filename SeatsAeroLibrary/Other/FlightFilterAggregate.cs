using SeatsAeroLibrary.Models.Entities;
using SeatsAeroLibrary.Services;
using SeatsAeroLibrary.Services.FlightFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models
{
    public class FilterAggregate
    {
        internal List<IFlightFilter> Filters { get; set; }
        public IFilterAnalyzer FilterAnalyzer { get; set; }


        public FilterAggregate(IFilterAnalyzer filterAnalyzer, bool runAnalyzer = true)
        {
            this.FilterAnalyzer = filterAnalyzer;
            if (filterAnalyzer == null)
            {
                this.FilterAnalyzer = new FilterAnalyzer();
            }
            if (runAnalyzer)
            {
                FilterAnalyzer.AnalyzeFilters(this);
            }
        }

        public FilterAggregate(List<IFlightFilterFactory> filterFactories, IFilterAnalyzer filterAnalyzer) : this(filterAnalyzer, runAnalyzer:false)
        { 
            this.Filters = new List<IFlightFilter>();

            if (filterFactories is not null)
            {
                foreach (IFlightFilterFactory factory in filterFactories)
                {
                    this.Filters.Add(factory.CreateFilter());
                }
            }

            FilterAnalyzer.AnalyzeFilters(this);
        }

        public FilterAggregate(List<IFlightFilter> filters, IFilterAnalyzer filterAnalyzer) : this(filterAnalyzer, runAnalyzer: false)
        {
            this.Filters = filters;
            FilterAnalyzer.AnalyzeFilters(this);
        }

        public void AddFilter(IFlightFilter filter)
        {
            this.Filters.Add(filter);
            FilterAnalyzer.AnalyzeFilters(this);
        }

        public List<Flight> Filter(List<Flight> flights)
        {
            List<Flight> results = new List<Flight>(flights);

            foreach (IFlightFilter filter in Filters)
            {
                results = filter.Filter(results);
                if (results.Count == 0)
                {
                    return results;
                }
            }

            return results;
        }

        public static FilterAggregate GetDefaultAggregate(IFilterAnalyzer filterAnalyzer = null)
        {
            if (filterAnalyzer == null)
            {
                filterAnalyzer = new FilterAnalyzer();
            }

            FilterAggregate filterAggregate = new FilterAggregate(filterAnalyzer);
            return filterAggregate;
        }

        public static FilterAggregate CheckNullAggregate(FilterAggregate filterAggregate, IFilterAnalyzer filterAnalyzer = null)
        {
            if (filterAggregate == null)
            {
                return new FilterAggregate(filterAnalyzer);
            }
            return filterAggregate;
        }
    }
}
