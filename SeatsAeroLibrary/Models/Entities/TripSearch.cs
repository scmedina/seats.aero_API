using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.Entities
{
    public class TripSearch
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public List<SearchCriteria> SearchCriteria { get; set; }
        public IFilterAnalyzer FilterAnalyzer { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Contact})";
        }

        public static List<TripSearch> GetTripSearches(IEnumerable<TripSearchDataModel> searches, IFilterAnalyzer filterAnalyzer = null)
        {
            List<TripSearch> result = new List<TripSearch>();
            foreach (TripSearchDataModel search in searches)
            {
                result.Add(GetTripSearch(search));
            }
            return result;
        }

        private static TripSearch GetTripSearch(TripSearchDataModel search, IFilterAnalyzer filterAnalyzer = null)
        {
            TripSearch result = new TripSearch();
            result.ID = search.ID;
            result.Name = search.Name;
            result.Contact = search.Contact;
            result.SearchCriteria = Entities.SearchCriteria.GetSearchCriteria(search.SearchCriteria, filterAnalyzer);
            result.FilterAnalyzer = filterAnalyzer;
            return result;
        }
    }
}
