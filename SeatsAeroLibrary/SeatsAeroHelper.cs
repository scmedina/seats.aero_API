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
using SeatsAeroLibrary.API.Models;
using System.Diagnostics;
using SeatsAeroLibrary.Services.FlightFilters;
using SeatsAeroLibrary.Repositories;
using SeatsAeroLibrary.API;
using SeatsAeroLibrary.Services.API.Factories;

namespace SeatsAeroLibrary
{
    public class SeatsAeroHelper
    {
        private static ILogger _logger;
        private static IConfigSettings _configSettings;
        private static IMessenger _messenger;
        private static IStatisticsRepository _statisticsRepository;
        private static IAPIWithFiltersFactory _aPIWithFiltersFactory;

        public SeatsAeroHelper (ILogger logger, IConfigSettings configSettings, IMessenger messenger, IStatisticsRepository statisticsRepository, IAPIWithFiltersFactory aPIWithFiltersFactory)
        {
            _logger = logger;
            _configSettings = configSettings;
            _messenger = messenger;
            _statisticsRepository = statisticsRepository;
            _aPIWithFiltersFactory = aPIWithFiltersFactory;
        }

        public List<Flight> LoadAvailabilityAndFilterSync(MileageProgram mileageProgram, bool forceMostRecent = false,
            List<List<IFlightFilterFactory>> filterFactories = null)
        {
            Task<List<Flight>> flightsAsync = LoadAvailabilityAndFilter(mileageProgram, false, filterFactories);
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
                FilterAggregate filterAggregate = new FilterAggregate(theseFilterFactories, new FilterAnalyzer());
                results.AddRange(await LoadAvailabilityAndFilter(mileageProgram,forceMostRecent,filterAggregate));
            }

            results.Sort();
            return results;
        }
        public async Task<List<Flight>> LoadAvailabilityAndFilter(MileageProgram mileageProgram, bool forceMostRecent = false,
            FilterAggregate filterAggregate = null)
        {
            return await LoadAvailability(mileageProgram, forceMostRecent: forceMostRecent, filterAggregate: filterAggregate);
        }
        public List<Flight> FilterAvailability(List<AvailabilityDataModel> availableData, List<List<IFlightFilterFactory>> filterFactories = null)
        {
            Guard.AgainstNullOrEmptyList(availableData, nameof(availableData));
            CheckFilterFactories(filterFactories);

            List<Flight> results = new List<Flight>();

            foreach (List<IFlightFilterFactory> theseFilterFactories in filterFactories)
            {
                FilterAggregate filterAggregate = new FilterAggregate(theseFilterFactories, new FilterAnalyzer());
                List<Flight> flights = Flight.GetFilteredFlights(filterAggregate,availableData);
                results.AddRange(flights);
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
            bool forceMostRecent = false, FilterAggregate filterAggregate = null, IFilterAnalyzer filterAnalyzer = null)
        {
            _logger.Info($"Loading availability of: {mileageProgram}");

            filterAggregate = FilterAggregate.CheckNullAggregate(filterAggregate, filterAnalyzer);

            List<Flight> results = new List<Flight>();

            EnumHelper enumHelper = new EnumHelper();
            List<MileageProgram> programs = enumHelper.GetBitFlagList(mileageProgram);
            List<Task<List<AvailabilityDataModel>>> myTasks = new List<Task<List<AvailabilityDataModel>>>();
            foreach (MileageProgram thisProgram in programs)
            {
                Task<List<AvailabilityDataModel>> task = LoadAvailabilitySingle(thisProgram, forceMostRecent, filterAggregate);
                myTasks.Add(task);
                //await task;
            }

            Task.WaitAll(myTasks.ToArray());

            foreach (Task<List<AvailabilityDataModel>> task in myTasks)
            {
                AddFilteredFlights(filterAggregate, results, task.Result);
            }

            return results;
        }

        private static void AddFilteredFlights(FilterAggregate filterAggregate, List<Flight> results, List<AvailabilityDataModel> availableData)
        {
            if (availableData != null)
            {
                List<Flight> flights = Flight.GetFilteredFlights(filterAggregate, availableData);
                results.AddRange(flights);
            }
        }

        public async Task<List<AvailabilityDataModel>> LoadAvailabilitySingle(MileageProgram thisProgram, bool forceMostRecent = false, 
            FilterAggregate filterAggregate = null, IFilterAnalyzer filterAnalyzer = null)
        {

            filterAggregate = FilterAggregate.CheckNullAggregate(filterAggregate, filterAnalyzer);

            List<Services.FlightFilters.SourceFilter> sourceFilters = new List<Services.FlightFilters.SourceFilter>();
            FlightFiltersHelpers.GetFilters<Services.FlightFilters.SourceFilter>(filterAggregate.Filters, ref sourceFilters, df => true);
            filterAggregate.Filters.RemoveAll(fl => sourceFilters.Contains(fl));
            filterAggregate.Filters.Add(new Services.FlightFilters.SourceFilter(thisProgram));

            string json = ""; bool createFile = false;
            AvailabilitySnapshot fileSnapshot = new AvailabilitySnapshot();

            List<AvailabilityDataModel> availabilities = null;

            SeatsAeroAvailabilityAPI apiCall = _aPIWithFiltersFactory.CreateAPI<SeatsAeroAvailabilityAPI>(filterAggregate);
            List<Flight> result = await apiCall.QueryResults();

            return availabilities;
        }




        public async Task<List<Flight>> LoadSearch(bool forceMostRecent = false,
            FilterAggregate filterAggregate = null, IFilterAnalyzer filterAnalyzer = null)
        {
            filterAggregate = FilterAggregate.CheckNullAggregate(filterAggregate, filterAnalyzer);

            List<Flight> results = new List<Flight>();

            Task<List<AvailabilityDataModel>> task = LoadCacheSearchSingle(forceMostRecent, filterAggregate);
            task.Wait();
            AddFilteredFlights(filterAggregate, results, task.Result);

            return results;
        }
        public async Task<List<AvailabilityDataModel>> LoadCacheSearchSingle(bool forceMostRecent = false,
            FilterAggregate filterAggregate = null, IFilterAnalyzer filterAnalyzer = null)
        {
            filterAggregate = FilterAggregate.CheckNullAggregate(filterAggregate, filterAnalyzer);

            string json = ""; bool createFile = false;
            AvailabilitySnapshot fileSnapshot = new AvailabilitySnapshot();

            SeatsAeroCacheSearchAPI apiCall = _aPIWithFiltersFactory.CreateAPI<SeatsAeroCacheSearchAPI>(filterAggregate);

            List<AvailabilityDataModel> availabilities = null;
            if (forceMostRecent == true || fileSnapshot.TryFindValidSnapshot($"{apiCall.OriginAirports}_{apiCall.DestinationAirports}", ref json) == false)
            {
                _logger.Info($"Querying Availability API Result: {apiCall.OriginAirports} > {apiCall.DestinationAirports}");

                List<Flight> result = await apiCall.QueryResults();
                createFile = true;
                //fileSnapshot.SaveSnapshot(mileageProgram, json);
            }

            _logger.Info($"Availability API Result Completed: {apiCall.OriginAirports} > {apiCall.DestinationAirports}");

            // Added to save the file in formatted JSON.
            if (createFile = true)
            {
                string fileName = fileSnapshot.GetFileNameByTypeAndDate($"{apiCall.OriginAirports}_{apiCall.DestinationAirports}", DateTime.Now);
                fileSnapshot.SaveSnapshot(availabilities, fileName);
            }

            return availabilities;
        }

        public async Task SaveRandomAvailabilityData(MileageProgram mileageProgram, bool forceMostRecent = false,  int countPerProgram = 250)
        {
            _logger.Info($"Saving availability of: {mileageProgram}");

            EnumHelper enumHelper = new EnumHelper();
            AvailabilitySnapshot fileSnapshot = new AvailabilitySnapshot();
            List<MileageProgram> programs = enumHelper.GetBitFlagList(mileageProgram);
            Services.FlightFilters.SourceFilter sourceFilter = null;
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
                    fileSnapshot.SaveSnapshot( randomAvailabilities, fileName);
                }
            }
        }

        
        
    }
}