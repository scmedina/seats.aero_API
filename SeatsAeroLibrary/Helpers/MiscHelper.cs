using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Helpers
{
    public class MiscHelper
    {

        public static T GetPropertyValue<T>(object myObject, string propertyName, T defaultValue)
        {
            Type type = myObject.GetType();
            PropertyInfo propertyInfo = type.GetProperty(propertyName);
            if (propertyInfo != null)
            {
                return (T)propertyInfo.GetValue(myObject);
            }
            return defaultValue;
        }

        public static bool IfNotComparable<T>(T leftVal, T rightVal, out int result) where T : IComparable<T>
        {
            result = leftVal.CompareTo(rightVal);
            return result == 0;
        }
        public static bool IfNotComparable<T,J>(J leftVal, J rightVal, Func<J, T> converter, out int result) where T : IComparable<T>
        {
            return IfNotComparable(converter(leftVal), converter(rightVal), out result);
        }

    }
}
