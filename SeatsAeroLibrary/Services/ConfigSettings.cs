using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SeatsAeroLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services
{
    public class ConfigSettings : IConfigSettings
    {

        public string APIKey { get; set; }
        public string OutputDirectory { get; set; }
        private string _connectionString { get; set; }

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
            OutputDirectory = (string)builder.Configuration.GetValue(typeof(string), "OutputDirectory");
            _connectionString = (string)builder.Configuration.GetConnectionString("DefaultConnection");
            

            if (String.IsNullOrEmpty(OutputDirectory))
            {
                OutputDirectory = Environment.GetEnvironmentVariable("Temp");
            }
            
            Guard.AgainstNullOrEmptyResultString(APIKey, nameof(APIKey));
            _isLoaded = true;
        }

        public string GetConnectionString()
        {
            return _connectionString;

        }
    }
}
