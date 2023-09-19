using SeatsAeroLibrary.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.Entities
{
    public class Trip
    {
        public string Name { get; set; }
        public string Contact { get; set; }
        public List<SearchCriteria> SearchCriteria { get; set; }
    }
}
