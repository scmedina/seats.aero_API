using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeatsAeroLibrary;
using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Models.Entities;
using SeatsAeroLibrary.Services;
using System;
using System.Reflection;
using Autofac;
using SeatsAeroLibrary.Models.Types;
using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using SeatsAeroLibrary.API.Models;
using System.Collections.Generic;
using SeatsAeroLibrary.Services.FlightFactories;
using SeatsAeroLibrary.Repositories;
using SeatsAeroLibrary.Services.API.Factories;

namespace SeatsAeroTests
{
    [TestClass]
    public class FilterResultsTests
    {
        private static ILogger _logger;
        private static IMessenger _messenger;
        private static IConfigSettings _configSettings;
        private static IStatisticsRepository _statisticsRepository;
        private static IAPIWithFiltersFactory _aPIWithFiltersFactory;

        static FilterResultsTests()
        {

            using (var scope = Services.TestServiceContainer.BuildContainer().BeginLifetimeScope())
            {
                _logger = scope.Resolve<ILogger>();
                _messenger = scope.Resolve<IMessenger>();
                _configSettings = scope.Resolve<IConfigSettings>();
                _statisticsRepository = scope.Resolve<IStatisticsRepository>();
                _aPIWithFiltersFactory = scope.Resolve<IAPIWithFiltersFactory>();
            }

        }

        [TestMethod]
        public void TestMethod1()
        {
            List<AvailabilityDataModel> data = GetAvailabilities();

            List<IFlightFilterFactory> filterFactories = new List<IFlightFilterFactory>();
            SeatType seatTypes = SeatType.First | SeatType.PremiumEconomy | SeatType.Business;
            filterFactories.Add(new SeatAvailabilityFilterFactory(1));
            filterFactories.Add(new DirectFilterFactory(true));
            filterFactories.Add(new MaxMileageCostFilterFactory(100000, true));
            filterFactories.Add(new DateFilterFactory(new DateTime(2023,10,7)));
            filterFactories.Add(new DateFilterFactory(new DateTime(2023, 10, 8), isEndDate: true));
            LocationByType houstonLocation = new LocationByType("BOM", SeatsAeroLibrary.Helpers.LocationType.Airport);
            filterFactories.Add(new LocationFilterFactory(
                new List<SeatsAeroLibrary.Models.Entities.LocationByType> { houstonLocation },
                isDestination: false
                ));

            SeatsAeroHelper seatsAeroInfo = new SeatsAeroHelper(_logger, _configSettings,_messenger, _statisticsRepository, _aPIWithFiltersFactory);

            List<Flight> flights = seatsAeroInfo.FilterAvailability(data, new List<List<IFlightFilterFactory>> { filterFactories });
        }

        [TestMethod]
        public void TestJsonStuff()
        {

            string json = FileIO.ReadEmbeddedResource("Data.test.json", Assembly.GetExecutingAssembly());

            var result = JsonSerializer.Deserialize<AvailabilityResultDataModel>(json);
        }


       // [TestMethod]
        public void HoustonCacheSearch()
        {
            List<List<IFlightFilterFactory>> allFilterFactories = new List<List<IFlightFilterFactory>>();
            List<IFlightFilterFactory> filterFactories1 = new List<IFlightFilterFactory>();
            allFilterFactories.Add(filterFactories1);
            SeatType seatTypes = SeatType.First | SeatType.Business | SeatType.PremiumEconomy;
            filterFactories1.Add(new LocationFilterFactory("IAH,HOU", isDestination:false));
            filterFactories1.Add(new LocationFilterFactory("FRA,LHR,CDG,AMS,MUC,HNL", isDestination: true));
            filterFactories1.Add(new SeatAvailabilityFilterFactory(2));
            filterFactories1.Add(new DirectFilterFactory(true));
            filterFactories1.Add(new MaxMileageCostFilterFactory(100000, true));
            FilterAggregate filterAggregate = new FilterAggregate(filterFactories1, new FilterAnalyzer());

            SeatsAeroHelper seatsAeroInfo = new SeatsAeroHelper(_logger, _configSettings, _messenger, _statisticsRepository, _aPIWithFiltersFactory);
            DateTime timer = DateTime.Now;
            Task<List<Flight>> task = seatsAeroInfo.LoadSearch(filterAggregate: filterAggregate);
            task.Wait();
            List<Flight> flights = task.Result;


            double totalTime = (DateTime.Now - timer).TotalMilliseconds;
            string filePath = $@"{Environment.GetEnvironmentVariable("Temp")}\\seats_aero_flights_[dateStamp]_[timeStamp]";
            filePath = filePath.Replace("[dateStamp]", DateTime.Now.ToString("yyyyMMdd"));
            filePath = filePath.Replace("[timeStamp]", DateTime.Now.ToString("HHmmss"));
            FileIO.ExportJsonListFile(flights, filePath + ".json");

            FileIO.SaveStringToFile(Flight.GetAsCSVString(flights), filePath + ".csv");
        }

        //[TestMethod]
        public void HoustonAvailabilitySearch()
        {
            List <List<IFlightFilterFactory>> allFilterFactories = new List<List<IFlightFilterFactory>>();
            List<IFlightFilterFactory> filterFactories1 = new List<IFlightFilterFactory>();
            allFilterFactories.Add(filterFactories1);
            SeatType seatTypes = SeatType.First | SeatType.Business | SeatType.PremiumEconomy;
            filterFactories1.Add(new SeatAvailabilityFilterFactory( 2));
            filterFactories1.Add(new DirectFilterFactory(true));
            filterFactories1.Add(new MaxMileageCostFilterFactory(100000, true));
            filterFactories1.Add(new LocationFilterFactory(
                new List<LocationByType> { new LocationByType("IAH") },
                isDestination: false
                ));
            LocationFilterFactory destination = new LocationFilterFactory(
                new List<LocationByType> {
                    new LocationByType(RegionName.Europe),
                    new LocationByType(RegionName.Africa),
                    new LocationByType(RegionName.Asia),
                    new LocationByType(RegionName.Oceania),
                    new LocationByType(RegionName.SouthAmerica)},
                isDestination: true);
            filterFactories1.Add(destination);


            List<IFlightFilterFactory> filterFactories2 = new List<IFlightFilterFactory>();
            allFilterFactories.Add(filterFactories2);
            filterFactories2.AddRange(filterFactories1);
            filterFactories2.Remove(destination);
            filterFactories2.Add(new LocationFilterFactory(
                new List<LocationByType> {
                    new LocationByType("HNL")},
                isDestination: true));

            SeatsAeroHelper seatsAeroInfo = new SeatsAeroHelper(_logger, _configSettings, _messenger, _statisticsRepository, _aPIWithFiltersFactory);
            DateTime timer = DateTime.Now;
            List<Flight> flights = seatsAeroInfo.LoadAvailabilityAndFilterSync(MileageProgram.united, false, allFilterFactories);

            double totalTime = (DateTime.Now - timer).TotalMilliseconds;
            string filePath = $@"{Environment.GetEnvironmentVariable("Temp")}\\seats_aero_flights_[dateStamp]_[timeStamp]";
            filePath = filePath.Replace("[dateStamp]", DateTime.Now.ToString("yyyyMMdd"));
            filePath = filePath.Replace("[timeStamp]", DateTime.Now.ToString("HHmmss"));
            FileIO.ExportJsonListFile(flights, filePath + ".json");

            FileIO.SaveStringToFile(Flight.GetAsCSVString(flights), filePath + ".csv");
        }

       // [TestMethod]
        public void AtlantaSearch()
        {

            List<List<IFlightFilterFactory>> allFilterFactories = new List<List<IFlightFilterFactory>>();
            List<IFlightFilterFactory> filterFactories1 = new List<IFlightFilterFactory>();
            allFilterFactories.Add(filterFactories1);
            SeatType seatTypes = SeatType.Any;
            //filterFactories1.Add(new SeatAvailabilityFilterFactory(seatTypes, 2));
            //filterFactories1.Add(new DirectFilterFactory(seatTypes, true));
            //filterFactories1.Add(new MaxMileageCostFilterFactory(seatTypes, 100000, true));
            filterFactories1.Add(new LocationFilterFactory(
                new List<LocationByType> { new LocationByType("ATL") },
                isDestination: false
                ));
            LocationFilterFactory destination = new LocationFilterFactory(
                new List<LocationByType> {
                    new LocationByType("SJU")},
                isDestination: true);
            filterFactories1.Add(destination);

            SeatsAeroHelper seatsAeroInfo = new SeatsAeroHelper(_logger, _configSettings, _messenger, _statisticsRepository, _aPIWithFiltersFactory);
            List<Flight> flights = seatsAeroInfo.LoadAvailabilityAndFilterSync(MileageProgram.all, false, allFilterFactories);

            string filePath = $@"{Environment.GetEnvironmentVariable("Temp")}\\seats_aero_flights_[dateStamp]_[timeStamp]";
            filePath = filePath.Replace("[dateStamp]", DateTime.Now.ToString("yyyyMMdd"));
            filePath = filePath.Replace("[timeStamp]", DateTime.Now.ToString("HHmmss"));

            FileIO.SaveStringToFile(Flight.GetAsCSVString(flights), filePath + ".csv");
        }

        //[TestMethod]
        public void SaveRandomTestData()
        {
            SeatsAeroHelper seatsAeroInfo = new SeatsAeroHelper(_logger, _configSettings, _messenger, _statisticsRepository, _aPIWithFiltersFactory);

            Task thisTask = seatsAeroInfo.SaveRandomAvailabilityData(MileageProgram.eurobonus, false,100);
            thisTask.Wait();
        }

        private List<AvailabilityDataModel> GetAvailabilities()
        {
            List<string> fileNames = new List<string>() { "seats_aero_american_20230909.json", "seats_aero_lifemiles_20230909.json", "seats_aero_velocity_20230909.json" };
            AvailabilitySnapshot snapshot = new AvailabilitySnapshot();

            List<AvailabilityDataModel> results = new List<AvailabilityDataModel>();
            foreach (var file in fileNames)
            {
                string json = FileIO.ReadEmbeddedResource("Data."+file, Assembly.GetExecutingAssembly());
                List<AvailabilityDataModel> availabilityData = snapshot.LoadAvailabilityByFileContents(json);
                if (availabilityData != null )
                {
                    results.AddRange(availabilityData);
                }
            }
            return results;
        }
    }
}
