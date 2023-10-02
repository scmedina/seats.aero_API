using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.Sort
{
    public class SortCriteria<T> where T : Enum
    {
        public T Field { get; set; }
        public SortDirection Direction { get; set; }

        public SortCriteria(T field, SortDirection direction)
        {
            Field = field;
            Direction = direction;
        }
    }
}
