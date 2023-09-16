using Microsoft.Extensions.Hosting;
using SeatsAeroLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services
{
    public interface IConfigSettings
    {
        public string APIKey { get; set; }
        public void Load();
    }
}
