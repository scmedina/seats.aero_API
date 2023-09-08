﻿using RestSharp;
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
using SeatsAeroLibrary.Models.Data;

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

        public async void LoadAvailability(MileageProgram mileageProgram, bool forceMostRecent = false,
            List<IFlightFilterFactory> filterFactories = null)
        {
            _logger.Info($"Loading availability of: {mileageProgram}");

            EnumHelper enumHelper = new EnumHelper();
            List<MileageProgram> programs = enumHelper.GetBitFlagList(mileageProgram);
            foreach (MileageProgram thisProgram in programs)
            {
                LoadAvailabilitySingle(thisProgram, forceMostRecent, filterFactories);
            }
        }
        public async void LoadAvailabilitySingle(MileageProgram mileageProgram, bool forceMostRecent = false,
            List<IFlightFilterFactory> filterFactories = null)
        {
            Guard.AgainstMultipleSources(mileageProgram, nameof(mileageProgram));

            string json = "";
            FileSnapshot fileSnapshot = new FileSnapshot();

            if (forceMostRecent == true ||  fileSnapshot.TryFindValidSnapshot(mileageProgram, ref json) == false)
            {
                json = await TryGetAPIAvailabilityResults(mileageProgram);
            }

            Guard.AgainstNullOrEmptyResultString(json, nameof(json));

            List<AvailabilityDataModel> availabilities = JsonSerializer.Deserialize<List<AvailabilityDataModel>>(json);
            Guard.AgainstNullOrEmptyList(availabilities, nameof(availabilities));

            List<Flight> flights = availabilities.Select(value => new Flight(value)).ToList();

            FilterAggregate filterAggregate = new FilterAggregate(filterFactories);
            List<Flight> filteredFlights = filterAggregate.Filter(flights);

            //System.Diagnostics.Debug.WriteLine("{0}", jsonResults);
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

            FileSnapshot fileSnapshot = new FileSnapshot();
            string filePath = FileSnapshot.SnapshotFileDirectory + fileSnapshot.GetFileNameBySourceAndDate(mileageProgram, DateTime.Now);
            System.IO.File.WriteAllText(filePath, results);

            return results;
        }

        
    }
}