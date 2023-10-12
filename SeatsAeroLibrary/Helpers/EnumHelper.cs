using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Helpers
{
    public class EnumHelper
    {
        public List<T> GetBitFlagList<T>(T values) where T: Enum
        {
            return GetBitFlagList<T>().Where(value => values.HasFlag(value)).ToList();
        }

        public List<T> GetEnumList<T>(string valuesString) where T : Enum
        {
            return GetEnumList(valuesString, ParseEnum<T>);
        }
        public List<T> GetEnumList<T>(string valuesString, Func<string,T> parseEnumFunc) where T : Enum
        {
            List<T> result = new List<T>();
            string[] valuesArray = valuesString.Split(',');
            foreach (string valueString in valuesArray)
            {
                result.Add(parseEnumFunc(valueString));
            }
            return result;
        }

        public static T ParseEnum<T>(string valueString) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), valueString);
        }


        public List<T> GetBitFlagList<T>() where T : Enum
        {
            Guard.AgainstNonFlagEnumType(typeof(T), nameof(T));

            return Enum.GetValues(typeof(T)).AsQueryable().Cast<T>().ToList().Where(value => IsSingleBitValue(value)).ToList();
        }
        public T GetHighestBitValue<T>() where T : Enum
        {
            T[] values = (T[])Enum.GetValues(typeof(T));
            T highestBitValue = default(T);

            foreach (T value in values)
            {
                if (IsSingleBitValue(value) == true)
                {
                    highestBitValue = value;
                }
            }

            return highestBitValue;
        }

        public bool IsSingleBitValue(Enum value)
        {
            return IsPowerOf2(Convert.ToInt32(value));
        }

        bool IsPowerOf2(int num)
        {
            return num > 0 && (num & num - 1) == 0;
        }

        public bool IsFlagsEnum(Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("The provided type is not an enum.");
            }
            
            return enumType.GetCustomAttributes(typeof(FlagsAttribute), false).Length > 0;
        }
    }

    
}
