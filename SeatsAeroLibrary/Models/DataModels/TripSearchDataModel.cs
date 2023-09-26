using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.DataModels
{
    public class TripSearchDataModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public List<SearchCriteriaDataModel> SearchCriteria { get; set; }
    }

}
