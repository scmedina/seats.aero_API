using SeatsAeroLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.API.Models
{
    public class AvailabilityResultDataModel
    {
        public List<AvailabilityDataModel> data { get; set; }
        public int count { get; set; }
        public bool hasMore { get; set; }
        public string moreURL { get; set; }
        public long cursor { get; set; }
    }
}
