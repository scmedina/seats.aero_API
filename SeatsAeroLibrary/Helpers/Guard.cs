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
            else if (program == MileageProgram.None)
            {
                throw new ArgumentException($"{argumentName} cannot be None.");
            }
        }

        public static void AgainstInvalidSource(string argument, string argumentName, out MileageProgram program)
        {
            if (Enum.TryParse(argument,out program) == false)
            {
                throw new ArgumentException($"{argumentName} is not a valid MileageProgram source: {argument}");
            }
        }
        public static void AgainstInvalidSeatType(string argument, string argumentName, out SeatType seatType)
        {
            if (Enum.TryParse(argument, out seatType) == false)
            {
                throw new ArgumentException($"{argumentName} is not a valid SeatType: {argument}");
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

        public static void AgainstNull(object value, string argumentName)
        {
            if (value is null)
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

        internal static void AgainstMissingDictionaryKeys(Dictionary<string, string> dictionary, string[] requiredParams, 
            string dictionaryArgName, string requiredParamsArgName)
        {
            if (requiredParams is null || requiredParams.ToList().Count == 0)
            {
                return;
            }

            if (dictionary is null)
            {
                throw new ArgumentNullException(dictionaryArgName);
            }
            else if (dictionary.Count == 0)
            {
                throw new ArgumentException($"{dictionaryArgName} is empty.");
            }

            foreach (string requiredParam in requiredParams)
            {
                if (dictionary.ContainsKey(requiredParam) == false)
                {
                    throw new ArgumentException($"{dictionaryArgName} is missing the required key {requiredParam} from {requiredParamsArgName}");
                }
            }
        }

        internal static void AgainstInvalidDateRange(DateTime startDate, DateTime endDate, string startDateArgName, string endDateArgName)
        {
            if (endDate< startDate)
            {
                throw new ArgumentException($"{startDateArgName} must be greater than or equal to {endDateArgName}.");
            }
        }

        public static void AgainstFailure(bool argument, string argumentName)
        {
            if (argument == false)
            { 
                throw new ArgumentException($"{argumentName} is a failure.");
            }
        }   
    }
}
