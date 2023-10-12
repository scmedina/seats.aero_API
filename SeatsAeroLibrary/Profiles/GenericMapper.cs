using SeatsAeroLibrary.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Profiles
{
    public class GenericMapper
    {
        public static void Map<TFrom, TTo>(TFrom source, TTo target)
        {
            PropertyInfo[] sourceProperties = typeof(TFrom).GetProperties();
            PropertyInfo[] targetProperties = typeof(TTo).GetProperties();

            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                PropertyInfo targetProperty = Array.Find(targetProperties, p => p.Name == sourceProperty.Name && p.PropertyType == sourceProperty.PropertyType);

                if (targetProperty != null && targetProperty.CanWrite)
                {
                    object value = sourceProperty.GetValue(source);
                    targetProperty.SetValue(target, value);
                }
            }
        }

        internal static T Map<T>(FlightRecordDataModel entity)
        {
            T result = default(T);
            Map(entity, result);
            return result;
        }
    }
}
