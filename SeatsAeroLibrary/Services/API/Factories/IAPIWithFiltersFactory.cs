using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Services.API.APICall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.API.Factories
{
    public interface IAPIWithFiltersFactory
    {
        public TApi CreateAPI<TApi>(FilterAggregate filterAggregate) where TApi : IAPIWithFilters, new();
    }
}
