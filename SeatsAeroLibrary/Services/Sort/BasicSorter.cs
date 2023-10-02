using Autofac;
using Microsoft.VisualBasic;
using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.Sort
{
    public abstract class BasicSorter<T, U> where T : class where U : Enum
    {
        public IEnumerable<T> SortTs(IEnumerable<T> objects, List<SortCriteria<U>> sortCriteria)
        {
            IOrderedEnumerable<T> orderedTs = null;

            foreach (SortCriteria<U> criteria in sortCriteria)
            {
                orderedTs = ApplySort(objects, orderedTs, flight => GetFieldValue(criteria.Field, flight), criteria.Direction);
            }

            return orderedTs ?? objects;
        }

        private IOrderedEnumerable<T> ApplySort(IEnumerable<T> objects, IOrderedEnumerable<T> orderedTs, Func<T, object> keySelector, SortDirection direction)
        {
            return orderedTs == null
                ? direction == SortDirection.Ascending ? objects.OrderBy(keySelector) : objects.OrderByDescending(keySelector)
                : direction == SortDirection.Ascending ? orderedTs.ThenBy(keySelector) : orderedTs.ThenByDescending(keySelector);
        }

        protected abstract object GetFieldValue(U field, T flight);

        public static IEnumerable<T> SortTs<T,U>(IList<T> objects, string sortFields, string sortDirections) where T : class where U : Enum
        {
            if(objects == null || objects.Count <=1 || String.IsNullOrWhiteSpace(sortFields) || sortDirections == null)
            {
                return objects;
            }

            ISorter<T, U> sorter = null;
            using (var scope = ServicesContainer.BuildContainer().BeginLifetimeScope())
            {
                sorter = scope.Resolve<ISorter<T, U>>();
            }

            EnumHelper enumHelper = new EnumHelper();
            List<SortDirection> sortDirectionList = enumHelper.GetEnumList<SortDirection>(sortDirections);
            List<U> sortFieldsList = enumHelper.GetEnumList<U>(sortFields);

            Guard.AgainstInvalidListCount(sortDirectionList, nameof(sortDirectionList), sortFieldsList, nameof(sortFields));

            List<SortCriteria<U>> sortCriteria = new List<SortCriteria<U>>();
            for (int i = 0; i < sortDirectionList.Count; i++)
            {
                sortCriteria.Add(new SortCriteria<U>(sortFieldsList[i], sortDirectionList[i]));
            }

            return sorter.SortTs(objects, sortCriteria);
        }


    }

}
