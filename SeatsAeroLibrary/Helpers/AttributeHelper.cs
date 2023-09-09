using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Helpers
{
    public class AttributeHelper
    {

        public static T? GetAttribute<T>(Type type)
        {
            var classAttributes = type.GetCustomAttributes(typeof(T), false);
            if (classAttributes.Length > 0)
            {
                T attribute = (T)classAttributes[0];
                return attribute;
            }
            return null;
        }
    }
}
