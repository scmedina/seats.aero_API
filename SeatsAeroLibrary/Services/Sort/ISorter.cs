using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.Sort
{
    public interface ISorter<T> where T : class 
    {
        List<Enum> GetFieldsList(string sortFields);
        public IEnumerable<T> SortTs(IEnumerable<T> objects, List<SortCriteria> sortCriteria);
    }
}
