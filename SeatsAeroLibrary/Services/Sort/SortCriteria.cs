using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.Sort
{
    public class SortCriteria
    {
        public Enum Field { get; set; }
        public SortDirection Direction { get; set; }

        public SortCriteria(Enum field, SortDirection direction)
        {
            Field = field;
            Direction = direction;
        }
    }
}
