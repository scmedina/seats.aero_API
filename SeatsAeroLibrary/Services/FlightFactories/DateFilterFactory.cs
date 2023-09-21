using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Services;
using SeatsAeroLibrary.Services.FlightFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.FlightFactories
{
    public class DateFilterFactory : IFlightFilterFactory
    {
        public DateTime Date { get; set; }
        public bool IsEndDate { get; set; }

        public DateFilterFactory(DateTime date, bool isEndDate = false)
        {
            Date = date;
            IsEndDate = isEndDate;
        }

        public IFlightFilter CreateFilter()
        {
            return new FlightFilters.DateFilter(Date, IsEndDate);
        }

        public List<IFlightFilter> CreateFilters(SearchCriteriaDataModel searchCriteriaDataModel)
        {
            List<IFlightFilter> filters = new List<IFlightFilter>();

            if (searchCriteriaDataModel.StartDate != null) 
            {
                filters.Add(new DateFilter((DateTime)searchCriteriaDataModel.StartDate, isEndDate: false));
            }

            if (searchCriteriaDataModel.EndDate != null)
            {
                filters.Add(new DateFilter((DateTime)searchCriteriaDataModel.EndDate, isEndDate: true));
            }

            return filters;
        }
    }
}
