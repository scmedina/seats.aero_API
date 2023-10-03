using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Profiles
{
    public abstract class BasicMapper<T1,T2>
    {
        public abstract T2 Map(T1 source);
        public abstract T1 Map(T2 source);

        protected static IList<U1> Map<U1, U2>(IList<U2> sources, Func<U2,U1> mapMethod)
        {
            var collection = new List<U1>();

            foreach (var source in sources)
            {
                collection.Add(mapMethod(source));
            }

            return collection;
        }

        public virtual IList<T1> Map(IList<T2> sources)
        {
            return Map(sources, Map);
        }

        public virtual IList<T2> Map(IList<T1> sources)
        {
            return Map(sources, Map);
        }
    }
}
