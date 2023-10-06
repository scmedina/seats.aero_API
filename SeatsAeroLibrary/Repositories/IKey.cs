using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Repositories
{
    public interface IKey<TKey, TItem>
    {
        public TKey GetKey(TItem item);
        public TItem GetByKey(TKey key);
        public bool KeyExists(TKey key);
    }
}
