using SeatsAeroLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services
{
    public interface IFilterAnalyzer
    {
        public void AnalyzeFilters(FilterAggregate filter);
    }
}
