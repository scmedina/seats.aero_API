using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Helpers
{
    public abstract class AttributeHelper
    {

        public static T  GetAttribute<T>(Type type, T defaultVal = null) where T: Attribute
        {
            var classAttributes = type.GetCustomAttributes(typeof(T), false);
            if (classAttributes.Length > 0)
            {
                T attribute = (T)classAttributes[0];
                return attribute;
            }
            return defaultVal;
        }

        public static string GetDescription( Enum enumVal, string defaultVal = "")
        {
            var descriptionAttribute = GetAttribute<DescriptionAttribute>(enumVal.GetType());
            if (descriptionAttribute != null)
            {
                return descriptionAttribute.Description;
            }
            return defaultVal;
        }
    }
}
