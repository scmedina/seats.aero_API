using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using SeatsAeroLibrary.Models;

namespace SeatsAeroLibrary.Helpers
{
    public static class Guard
    {
        public static void AgainstMultipleSources(MileageProgram program, string argumentName)
        {
            EnumHelper enumHelper = new EnumHelper();
            if (enumHelper.IsSingleBitValue(program) == false)
            {
                throw new SingleMileageProgramRequiredException(program.ToString());
            }
        }

        public static void AgainstNonFlagEnumType(Type enumType, string argumentName)
        {
            EnumHelper enumHelper = new EnumHelper();
            if (enumHelper.IsFlagsEnum(enumType) == false)
            {
                throw new InvalidCastException($"{argumentName} is not a flags enum");
            }
        }

        public static void AgainstNullOrEmptyResultString(string value, string argumentName)
        {
            if (String.IsNullOrEmpty(value) == true)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
        public static void AgainstNullOrEmptyList<T>(List<T> value, string argumentName)
        {
            if (value is null)
            {
                throw new ArgumentNullException(argumentName);
            }
            else if (value.Count == 0) 
            {
                throw new ArgumentException($"{argumentName} is empty.");
            }
        }
    }
}
