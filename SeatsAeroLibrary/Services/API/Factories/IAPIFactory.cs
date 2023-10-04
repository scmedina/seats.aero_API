using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Services.API.APICall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.API.Factories
{
    public interface IAPIFactory
    {
        public TApi CreateAPI<TApi>() where TApi : IAPI, new();
    }
}
