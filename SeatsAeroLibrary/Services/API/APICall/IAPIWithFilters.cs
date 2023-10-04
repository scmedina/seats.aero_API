using SeatsAeroLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.API.APICall
{
    public interface IAPIWithFilters: IAPI
    {
        public void SetFilterAggregate(FilterAggregate filterAggregate);
    }
}
