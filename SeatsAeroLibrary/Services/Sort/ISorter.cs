using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.Sort
{
    public interface ISorter<T, U> where T : class where U : Enum
    {
        public IEnumerable<T> SortTs(IEnumerable<T> objects, List<SortCriteria<U>> sortCriteria);
    }
}
