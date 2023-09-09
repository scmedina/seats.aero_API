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
        public async Task<List<Flight>> LoadAvailabilityAndFilter(MileageProgram mileageProgram, bool forceMostRecent = false,
            List<IFlightFilterFactory> filterFactories = null)
        {
            List<AvailabilityDataModel> availableData = await LoadAvailability(mileageProgram, forceMostRecent);

            Guard.AgainstNullOrEmptyList(availableData, nameof(availableData));

            List<Flight> flights = availableData.Select(value => new Flight(value)).ToList();

            FilterAggregate filterAggregate = new FilterAggregate(filterFactories);
            List<Flight> filteredFlights = filterAggregate.Filter(flights);
            return filteredFlights;
        }

        public async Task<List<AvailabilityDataModel>> LoadAvailability(MileageProgram mileageProgram, bool forceMostRecent = false)
        {
            _logger.Info($"Loading availability of: {mileageProgram}");

            List<AvailabilityDataModel> results = new List<AvailabilityDataModel>();

            EnumHelper enumHelper = new EnumHelper();
            List<MileageProgram> programs = enumHelper.GetBitFlagList(mileageProgram);
            foreach (MileageProgram thisProgram in programs)
            {
                List< AvailabilityDataModel> availabilities = await LoadAvailabilitySingle(thisProgram, forceMostRecent);
                if (availabilities != null)
                {
                    results.AddRange(availabilities);
                }
            }
            return results;
        }
        public async Task<List<AvailabilityDataModel>> LoadAvailabilitySingle(MileageProgram mileageProgram, bool forceMostRecent = false)
        {
            Guard.AgainstMultipleSources(mileageProgram, nameof(mileageProgram));

            string json = "";
            FileSnapshot fileSnapshot = new FileSnapshot();

            if (forceMostRecent == true ||  fileSnapshot.TryFindValidSnapshot(mileageProgram, ref json) == false)
            {
                json = await TryGetAPIAvailabilityResults(mileageProgram);
                fileSnapshot.SaveSnapshot(mileageProgram, json);
            }

            Guard.AgainstNullOrEmptyResultString(json, nameof(json));

            List<AvailabilityDataModel> availabilities = JsonSerializer.Deserialize<List<AvailabilityDataModel>>(json);
            return availabilities;
        }

        public async Task SaveRandomAvailabilityData(MileageProgram mileageProgram, bool forceMostRecent = false,  int countPerProgram = 0)
        {
            _logger.Info($"Saving availability of: {mileageProgram}");

            EnumHelper enumHelper = new EnumHelper();
            FileSnapshot fileSnapshot = new FileSnapshot();
            List<MileageProgram> programs = enumHelper.GetBitFlagList(mileageProgram);
            foreach (MileageProgram thisProgram in programs)
            {
                List<AvailabilityDataModel> availabilities = await LoadAvailabilitySingle(thisProgram, forceMostRecent);
                if (availabilities != null)
                {
                    List<AvailabilityDataModel> randomAvailabilities = availabilities.GetRandomSubset(250);
                    string fileName = FileSnapshot.GetFileNameBySourceAndDate("seats_aero_[source]_[dateStamp].json", thisProgram, DateTime.Now);
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