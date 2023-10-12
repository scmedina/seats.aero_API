using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services
{
    public interface ISearchCriteriaService
    {
        public List<SearchCriteria> GetSearchCriteria(List<SearchCriteriaDataModel> searchCriteria, IFilterAnalyzer filterAnalyzer = null);
    }
}
