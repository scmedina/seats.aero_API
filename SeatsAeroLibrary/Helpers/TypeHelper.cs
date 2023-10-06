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

        public static T GetById<T,U>(Dictionary<U,T> entities, U id)
        {
            if (entities.ContainsKey(id))
            {
                return entities[id];
            }
            return default;
        }
        public static bool UpdateByID<T, U>(Dictionary<U, T> entities, U id, T entity)
        {
            if (entities.ContainsKey(id))
            {
                entities[id] = entity;
                return true;
            }
            return false;
        }
        public static bool DeleteByID<T, U>(Dictionary<U, T> entities, U id)
        {
            if (entities.ContainsKey(id))
            {
                entities.Remove(id);
                return true;
            }
            return false;
        }

        protected void BuildDictionary<T, U>(Dictionary<U, T> entities, List<T> elements,
            Func<T, U> getKey)
        {
            if (entities == null)
            {
                entities = new Dictionary<U, T>();
            }
            foreach (var element in elements)
            {
                if (entities.ContainsKey(getKey(element)))
                {
                    entities.Add(getKey(element), element);
                }
            }
        }
    }
}
