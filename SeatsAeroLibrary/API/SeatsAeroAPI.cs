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

namespace SeatsAeroLibrary.API
{
    public abstract class SeatsAeroAPI<T> where T : class
    {

        private const string _baseUrl = "https://seats.aero/api/";
        public string[] RequiredParams { get; set; }
        public Dictionary<string,string> QueryParams { get; set; }
        public string EndPoint { get; set; }

        public SeatsAeroAPI(string endPoint, string[] requiredParams, Dictionary<string, string> queryParams = null)
        {
            EndPoint = endPoint;
            RequiredParams = requiredParams;
            QueryParams = queryParams;
        }

        public async Task<T> QueryResults()
        {
            string json = await MakeApiRequestAsync();
            return JsonSerializer.Deserialize<T>(json);
        }

        protected async Task<string> MakeApiRequestAsync()
        {
            try
            {
                Guard.AgainstNullOrEmptyResultString(ConfigurationManager.AppSettings["ApiKey"], "ConfigurationManager.AppSettings[\"ApiKey\"]");
                Guard.AgainstMissingDictionaryKeys(QueryParams, RequiredParams, nameof(QueryParams), nameof(RequiredParams));

                // Build the request URL
                var requestUrl = $"{_baseUrl}/{EndPoint}";

                if (QueryParams != null && QueryParams.Count > 0)
                {
                    var queryString = string.Join("&", QueryParams.Select(kv => $"{kv.Key}={kv.Value}"));
                    requestUrl += $"?{queryString}";
                }

                // Send a GET request to the API
                var options = new RestClientOptions(requestUrl);
                var client = new RestClient(options);
                var request = new RestRequest("");
                request.AddHeader("accept", "application/json");
                request.AddHeader("Partner-Authorization", ConfigurationManager.AppSettings["ApiKey"]);
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

    }
}
