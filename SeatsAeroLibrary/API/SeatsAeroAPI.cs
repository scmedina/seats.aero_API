using RestSharp;
using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http;
using System.Text.Json;
using SeatsAeroLibrary.Services;
using Autofac;

namespace SeatsAeroLibrary.API
{
    public abstract class SeatsAeroAPI<T, U> where T : class where U : class
    {

        private const string _baseUrl = "https://seats.aero";
        public virtual string[] RequiredParams { get; set; }
        public virtual Dictionary<string,string> QueryParams { get; set; }
        public virtual string EndPoint { get; set; }
        public virtual bool HasCounter { get; set; } = false;

        protected IConfigSettings _configSettings { get; set; }

        public SeatsAeroAPI()
        {
            using (var scope = ServicesContainer.BuildContainer().BeginLifetimeScope())
            {
                _configSettings = scope.Resolve<IConfigSettings>();
            }
            _configSettings.Load();
        }

        public SeatsAeroAPI(string endPoint, string[] requiredParams, Dictionary<string, string> queryParams = null)  : this()
        {
            EndPoint = endPoint;
            RequiredParams = requiredParams;
            QueryParams = queryParams;
        }

        public virtual async Task<U> QueryResults()
        {
            APIResult<T, U> result = await QueryResults(null);

            return result.UData;
        }

        protected async Task<APIResult<T, U>> QueryResults(string requestUrl = null)
        {
            string json = "";
            if (String.IsNullOrEmpty(requestUrl))
            {
                json = await MakeApiRequestAsync();
            }
            else
            {
                json = await MakeApiRequestAsync(requestUrl);
            }
            T data = JsonSerializer.Deserialize<T>(json);
            U result = GetU(data);
            return new APIResult<T,U>(data,result);
        }

        protected abstract U GetU(T? data);

        protected async Task<string> MakeApiRequestAsync()
        {

            Guard.AgainstNull(_configSettings, nameof(_configSettings));
            Guard.AgainstNullOrEmptyResultString(_configSettings.APIKey, nameof(_configSettings.APIKey));
            Guard.AgainstMissingDictionaryKeys(QueryParams, RequiredParams, nameof(QueryParams), nameof(RequiredParams));

            // Build the request URL
            var endUrl = $"{EndPoint}";

            if (QueryParams != null && QueryParams.Count > 0)
            {
                var queryString = string.Join("&", QueryParams.Select(kv => $"{kv.Key}={kv.Value}"));
                endUrl += $"?{queryString}";
            }

            return await MakeApiRequestAsync(endUrl);
        }

        private async Task<string> MakeApiRequestAsync(string endURL)
        {
            Guard.AgainstNullOrEmptyResultString(endURL, nameof(endURL));
            string requestUrl = $"{_baseUrl}/{endURL}";
            try
            {
                // Send a GET request to the API
                var options = new RestClientOptions(requestUrl);
                var client = new RestClient(options);
                var request = new RestRequest("");
                request.AddHeader("accept", "application/json");
                request.AddHeader("Partner-Authorization", _configSettings.APIKey);
                var response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response JSON into the specified type T
                    return response.Content == null ? "" : response.Content;
                }
                else
                {
                    // Handle the API error response or exceptions here
                    // You can throw an exception or return an appropriate error message.
                    return default; // Change the return type to a nullable type if needed
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                // You can log the exception or throw it as needed.
                return default; // Change the return type to a nullable type if needed
            }
        }

        public class APIResult<T, U> where T : class where U : class
        {
            public T TData { get; set; }
            public U UData { get; set; }

            public APIResult(T tData, U uData)
            {
                TData = tData;
                UData = uData;
            }
        }
    }
}
