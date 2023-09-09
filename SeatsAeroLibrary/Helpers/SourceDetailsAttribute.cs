using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Helpers
{
    public class SourceDetailsAttribute : Attribute
    {
        public bool HasSeatCount { get; set; }
        public bool HasTripData { get; set; }

        public SourceDetailsAttribute(bool hasSeatCount, bool hasTripData)
        {
            HasSeatCount = hasSeatCount;
            HasTripData = hasTripData;
        }

        
    }
}
