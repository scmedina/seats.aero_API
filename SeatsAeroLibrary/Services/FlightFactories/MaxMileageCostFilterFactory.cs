using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Models.Entities;
using SeatsAeroLibrary.Services;
using SeatsAeroLibrary.Services.FlightFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.FlightFactories
{
    public class MaxMileageCostFilterFactory : IFlightFilterFactory
    {

        private int? _maxPoints;
        private bool _nonZero = false;

        public MaxMileageCostFilterFactory(bool nonZero = false)
        {
            _nonZero = nonZero;
        }

        public MaxMileageCostFilterFactory(int? maxPoints = null, bool nonZero = false) : this(nonZero)
        {
            _maxPoints = maxPoints;
        }

        public IFlightFilter CreateFilter()
        {
            return new MaxMileageCostFilter(_maxPoints, _nonZero);
        }

        public List<IFlightFilter> CreateFilters(SearchCriteria searchCriteria)
        {
            List<IFlightFilter> filters = new List<IFlightFilter>();
            filters.Add(new MaxMileageCostFilter(searchCriteria.MaxMileage, _nonZero));
            return filters;
        }
    }
}
