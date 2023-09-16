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
        public List<AvailabilityDataModel> Data { get; set; }
        public int Count { get; set; }
        public bool HasMore { get; set; }
        public string MoreURL { get; set; }
        public int Cursor { get; set; }
    }
}
