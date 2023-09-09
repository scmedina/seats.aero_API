using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Helpers
{
    public static class ListHelper
    {
        private static readonly Random random = new Random();
        public static List<T> GetRandomSubset<T>(this List<T> originalList, int numberOfElements)
        {
            if (numberOfElements < 0)
                throw new ArgumentOutOfRangeException(nameof(numberOfElements), "Number of elements cannot be negative.");

            if (numberOfElements >= originalList.Count)
                return originalList; // Return the entire list if numberOfElements is greater than or equal to the list size.

            // Shuffle the original list to randomize the order.
            List<T> shuffledList = originalList.OrderBy(x => random.Next()).ToList();

            // Take the first 'numberOfElements' elements from the shuffled list.
            List<T> subset = shuffledList.Take(numberOfElements).ToList();

            return subset;
        }
    }
}
