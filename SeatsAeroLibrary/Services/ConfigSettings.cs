using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SeatsAeroLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services
{
    public class ConfigSettings : IConfigSettings
    {

        public string APIKey { get; set; }
        private bool _isLoaded  = false;

        public void Load()
        {
            if (_isLoaded == true)
            {
                return;
            }

            HostApplicationBuilder builder = Host.CreateApplicationBuilder();

            builder.Configuration.Sources.Clear();
            IHostEnvironment env = builder.Environment;

            builder.Configuration
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

            APIKey = (string)builder.Configuration.GetValue(typeof(string), "ApiKey");

            Guard.AgainstNullOrEmptyResultString(APIKey, nameof(APIKey));
            _isLoaded = true;
        }
    }
}
