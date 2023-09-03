using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Helpers
{
    public class TypeHelper
    {
        public static List<Type> GetTypesImplementingInterface<T>()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            return assembly.GetTypes()
                .Where(type => typeof(T).IsAssignableFrom(type) && type.IsClass)
                .ToList();
        }
    }
}
