using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.Entities
{
    public class SearchCriteria
    {
        public FilterAggregate FilterAggregate { get; set; }


        public SearchCriteria(List<SearchCriteriaDataModel> searchCriteriaDatas) : this(searchCriteriaDatas, new FilterAnalyzer())
        {
        }
        public SearchCriteria(List<SearchCriteriaDataModel> searchCriteriaDatas, IFilterAnalyzer filterAnalyzer) 
        {

        }

    }
}
