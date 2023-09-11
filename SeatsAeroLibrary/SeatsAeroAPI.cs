using RestSharp;
using SeatsAeroLibrary.Models;
using System.Runtime.CompilerServices;
using System.Text;
using System;
using System.Collections.Generic;
using System.Text.Json;
using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Services;
using static System.Formats.Asn1.AsnWriter;
using Autofac;
using System.Net;
using System.ComponentModel;
using System.Collections.Specialized;

namespace SeatsAeroLibrary
{
    public class SeatsAeroAPI
    {
        private static ILogger _logger;
        private static IMessenger _messenger;

        public SeatsAeroAPI ()
        {
            using (var scope = ServicesContainer.BuildContainer().BeginLifetimeScope())
            {
                _logger = scope.Resolve<ILogger>();
                _messenger = scope.Resolve<IMessenger>();
            }
        }

        public List<Flight> LoadAvailabilityAndFilterSync(MileageProgram mileageProgram, bool forceMostRecent = false,
            List<List<IFlightFilterFactory>> filterFactories = null)
        {
            Task<List<Flight>> flightsAsync = LoadAvailabilityAndFilter(MileageProgram.all, false, filterFactories);
            flightsAsync.Wait();
            return flightsAsync.Result;
        }
        public async Task<List<Flight>> LoadAvailabilityAndFilter(MileageProgram mileageProgram, bool forceMostRecent = false,
            List<List<IFlightFilterFactory>> filterFactories = null)
        {
            CheckFilterFactories(filterFactories);

            List<Flight> results = new List<Flight>();
            foreach (List<IFlightFilterFactory> theseFilterFactories in filterFactories)
            {
                FilterAggregate filterAggregate = new FilterAggregate(theseFilterFactories);
                results.AddRange(await LoadAvailabilityAndFilter(mileageProgram,forceMostRecent,filterAggregate));
            }

            results.Sort();
            return results;
        }
        public async Task<List<Flight>> LoadAvailabilityAndFilter(MileageProgram mileageProgram, bool forceMostRecent = false,
            FilterAggregate filterAggregate = null)
        {
            return await LoadAvailability(mileageProgram, forceMostRecent, filterAggregate);
        }
        public List<Flight> FilterAvailability(List<AvailabilityDataModel> availableData, List<List<IFlightFilterFactory>> filterFactories = null)
        {
            Guard.AgainstNullOrEmptyList(availableData, nameof(availableData));
            CheckFilterFactories(filterFactories);

            List<Flight> results = new List<Flight>();
            List<Flight> flights = Flight.GetFlights(availableData);

            foreach (List<IFlightFilterFactory> theseFilterFactories in filterFactories)
            {
                FilterAggregate filterAggregate = new FilterAggregate(theseFilterFactories);
                results.AddRange(filterAggregate.Filter(flights));
            }

            return results;
        }

        protected static void CheckFilterFactories( List<List<IFlightFilterFactory>>  filterFactories)
        {
            if (filterFactories is null)
            {
                filterFactories = new List<List<IFlightFilterFactory>>();
                filterFactories.Add(null);
            }
        }

        public async Task<List<Flight>> LoadAvailability(MileageProgram mileageProgram, 
            bool forceMostRecent = false, FilterAggregate filterAggregate = null)
        {
            _logger.Info($"Loading availability of: {mileageProgram}");

            List<Flight> results = new List<Flight>();

            EnumHelper enumHelper = new EnumHelper();
            List<MileageProgram> programs = enumHelper.GetBitFlagList(mileageProgram);
            foreach (MileageProgram thisProgram in programs)
            {
                List<AvailabilityDataModel> availableData = await LoadAvailabilitySingle(thisProgram, forceMostRecent);
                if (availableData != null)
                {
                    List<Flight> flights = Flight.GetFlights(availableData);
                    List<Flight> filteredFlights = filterAggregate.Filter(flights);
                    results.AddRange(filteredFlights);
                }
            }
            return results;
        }
        public async Task<List<AvailabilityDataModel>> LoadAvailabilitySingle(MileageProgram mileageProgram, bool forceMostRecent = false)
        {
            Guard.AgainstMultipleSources(mileageProgram, nameof(mileageProgram));

            string json = ""; bool createFile = false;
            AvailabilitySnapshot fileSnapshot = new AvailabilitySnapshot();

            if (forceMostRecent == true ||  fileSnapshot.TryFindValidSnapshot(mileageProgram, ref json) == false)
            {
                json = await TryGetAPIAvailabilityResults(mileageProgram);
                createFile = true;
                //fileSnapshot.SaveSnapshot(mileageProgram, json);
            }

            Guard.AgainstNullOrEmptyResultString(json, nameof(json));

            List<AvailabilityDataModel> availabilities = JsonSerializer.Deserialize<List<AvailabilityDataModel>>(json);


            // Added to save the file in formatted JSON.
            if (createFile = true)
            {
                string fileName = fileSnapshot.GetFileNameBySourceAndDate(mileageProgram, DateTime.Now);
                fileSnapshot.SaveSnapshot(mileageProgram, availabilities, fileName);
            }

            return availabilities;
        }

        public async Task SaveRandomAvailabilityData(MileageProgram mileageProgram, bool forceMostRecent = false,  int countPerProgram = 250)
        {
            _logger.Info($"Saving availability of: {mileageProgram}");

            EnumHelper enumHelper = new EnumHelper();
            AvailabilitySnapshot fileSnapshot = new AvailabilitySnapshot();
            List<MileageProgram> programs = enumHelper.GetBitFlagList(mileageProgram);
            foreach (MileageProgram thisProgram in programs)
            {
                List<AvailabilityDataModel> availabilities = await LoadAvailabilitySingle(thisProgram, forceMostRecent);
                if (availabilities != null)
                {
                    List<AvailabilityDataModel> randomAvailabilities = new List<AvailabilityDataModel>(availabilities);
                    if (countPerProgram > 0)
                    {
                        randomAvailabilities = availabilities.GetRandomSubset(countPerProgram);
                    }
                    string fileName = AvailabilitySnapshot.GetFileNameBySourceAndDate("seats_aero_[source]_[dateStamp].json", thisProgram, DateTime.Now);
                    fileSnapshot.SaveSnapshot(thisProgram, randomAvailabilities, fileName);
                }
            }
        }


        private async Task<string> TryGetAPIAvailabilityResults(MileageProgram mileageProgram)
        {
            Guard.AgainstMultipleSources(mileageProgram, nameof(mileageProgram));

            var options = new RestClientOptions($"https://seats.aero/api/availability?source={mileageProgram.ToString()}");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            var response = await client.GetAsync(request);

            string results = response.Content == null ? "" : response.Content;

            return results;
        }

        
        
    }
}