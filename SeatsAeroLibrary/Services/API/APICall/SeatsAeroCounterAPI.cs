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
using SeatsAeroLibrary.API.Models;
using SeatsAeroLibrary.Repositories;
using SeatsAeroLibrary.Services.API.APICall;

namespace SeatsAeroLibrary.API
{
    public abstract class SeatsAeroCounterAP<T, U> : SeatsAeroAPI<T, List<U>> where T : class where U : class
    {
        protected abstract override List<U> GetU(T? data);

        public SeatsAeroCounterAP(string endPoint, string[] requiredParams, Dictionary<string, string> queryParams = null) : base(endPoint,requiredParams, queryParams) { }

        public override async Task<List<U>> QueryResults()
        {
            List<U> uData = new List<U>();
            APIResult<T, List<U>> apiResult = null;
            string requestUrl = null;
            do
            {
                apiResult = await QueryResults(requestUrl);
                uData.AddRange(apiResult.UData);
                requestUrl = GetMoreURL(apiResult);
            } while (ContinueCounter(apiResult));
            return uData;
        }

        protected abstract bool ContinueCounter(APIResult<T, List<U>> apiResult);

        protected abstract string GetMoreURL(APIResult<T, List<U>> apiResult);
    }
}
