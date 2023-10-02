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
    public abstract class BasicSorter<T> : ISorter<T> where T : class
    {

        public IEnumerable<T> SortTs(IEnumerable<T> objects, List<SortCriteria> sortCriteria)
        {
            IOrderedEnumerable<T> orderedTs = null;

            foreach (SortCriteria criteria in sortCriteria)
            {
                orderedTs = ApplySort(objects, orderedTs, flight => GetFieldValue(criteria.Field, flight), criteria.Direction);
            }

            return orderedTs ?? objects;
        }

        private IOrderedEnumerable<T> ApplySort(IEnumerable<T> objects, IOrderedEnumerable<T> orderedTs, Func<T, object> keySelector, SortDirection direction)
        {
            if (direction == SortDirection.Asc)
            {
                return objects.OrderBy(keySelector);
            }
            else
            {
                return objects.OrderByDescending(keySelector);
            }
        }

        protected abstract object GetFieldValue(Enum field, T flight);

        public static IEnumerable<T> SortTs<T>(IList<T> objects, string sortFields, string sortDirections) where T : class
        {
            if(objects == null || objects.Count <=1 || String.IsNullOrWhiteSpace(sortFields) || sortDirections == null)
            {
                return objects;
            }

            ISorter<T> sorter = null;
            using (var scope = ServicesContainer.BuildContainer().BeginLifetimeScope())
            {
                sorter = scope.Resolve<ISorter<T>>();
            }

            EnumHelper enumHelper = new EnumHelper();
            List<SortDirection> sortDirectionList = enumHelper.GetEnumList<SortDirection>(sortDirections);
            List<Enum> sortFieldsList = sorter.GetFieldsList(sortFields);

            Guard.AgainstInvalidListCount(sortDirectionList, nameof(sortDirectionList), sortFieldsList, nameof(sortFields));

            List<SortCriteria> sortCriteria = new List<SortCriteria>();
            for (int i = 0; i < sortDirectionList.Count; i++)
            {
                sortCriteria.Add(new SortCriteria(sortFieldsList[i], sortDirectionList[i]));
            }

            return sorter.SortTs(objects, sortCriteria);
        }

        public abstract List<Enum> GetFieldsList(string sortFields);
    }

}
