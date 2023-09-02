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
