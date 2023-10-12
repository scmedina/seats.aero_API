using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services
{
    public interface ITripSearchService
    {
        public List<TripSearch> GetTripSearches(IEnumerable<TripSearchDataModel> searches, IFilterAnalyzer filterAnalyzer = null);
        public void GetAllFlightsFromCachedSearches(List<TripSearch> trips);
    }
}
