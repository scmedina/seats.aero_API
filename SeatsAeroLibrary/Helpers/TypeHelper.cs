using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
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

        public static int NullComparable<T>(T left, T right) where T: IComparable
        {
            if (left == null && right == null)
            {
                return 0; // Both are null, consider them equal
            }
            else if (left == null)
            {
                return -1; // Current Airline is null, consider it smaller than right
            }
            else if (right == null)
            {
                return 1; // Other Airline is null, consider it smaller than current Airline
            }
            else
            {
                return left.CompareTo(right);
            }
        }
    }
}
