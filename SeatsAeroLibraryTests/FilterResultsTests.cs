using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeatsAeroLibrary;
using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Models.Entities;
using SeatsAeroLibrary.Models.FlightFactories;
using SeatsAeroLibrary.Services;
using System;
using System.Reflection;
using Autofac;
using SeatsAeroLibrary.Models.Types;

namespace SeatsAeroTests
{
    [TestClass]
    public class FilterResultsTests
    {
        private static ILogger _logger;
        private static IMessenger _messenger;

        static FilterResultsTests()
        {

            using (var scope = Services.TestServiceContainer.BuildContainer().BeginLifetimeScope())
            {
                _logger = scope.Resolve<ILogger>();
                _messenger = scope.Resolve<IMessenger>();
            }

        }

        [TestMethod]
        public void TestMethod1()
        {
            List<AvailabilityDataModel> data = GetAvailabilities();

            List<IFlightFilterFactory> filterFactories = new List<IFlightFilterFactory>();
            SeatType seatTypes = SeatType.FFirstClass | SeatType.WPremiumEconomy | SeatType.JBusiness;
            filterFactories.Add(new SeatAvailabilityFilterFactory(seatTypes, 1));
            filterFactories.Add(new DirectFilterFactory(seatTypes, true));
            filterFactories.Add(new MaxMileageCostFilterFactory(seatTypes, 100000, true));
            filterFactories.Add(new DateFilterFactory(new DateTime(2023,10,7)));
            filterFactories.Add(new DateFilterFactory(new DateTime(2023, 10, 8), isEndDate: true));
            LocationByType houstonLocation = new LocationByType("BOM", SeatsAeroLibrary.Helpers.LocationType.Airport);
            filterFactories.Add(new LocationFilterFactory(
                new List<SeatsAeroLibrary.Models.Entities.LocationByType> { houstonLocation },
                isDestination: false
                ));

            SeatsAeroAPI seatsAeroInfo = new SeatsAeroAPI();

            List<Flight> flights = seatsAeroInfo.FilterAvailability(data, filterFactories);
        }

        [TestMethod]
        public void HoustonSearch()
        {

            List<IFlightFilterFactory> filterFactories = new List<IFlightFilterFactory>();
            SeatType seatTypes = SeatType.FFirstClass | SeatType.JBusiness | SeatType.WPremiumEconomy;
            filterFactories.Add(new SeatAvailabilityFilterFactory(seatTypes, 2));
            filterFactories.Add(new DirectFilterFactory(seatTypes, true));
            filterFactories.Add(new MaxMileageCostFilterFactory(seatTypes, 100000, true));
            filterFactories.Add(new LocationFilterFactory(
                new List<LocationByType> { new LocationByType("IAH") },
                isDestination: false
                ));

            filterFactories.Add(new LocationFilterFactory(
                new List<LocationByType> {
                    new LocationByType(RegionName.Europe),
                    new LocationByType(RegionName.Africa),
                    new LocationByType(RegionName.Asia),
                    new LocationByType(RegionName.Oceania),
                    new LocationByType(RegionName.SouthAmerica)},
                isDestination: true
                ));

            SeatsAeroAPI seatsAeroInfo = new SeatsAeroAPI();
            Task<List<Flight>> flightsAsync = seatsAeroInfo.LoadAvailabilityAndFilter(MileageProgram.all, false, filterFactories);
            flightsAsync.Wait();
            List<Flight> flights = flightsAsync.Result;

            var routes = Route.GetRoutes(flights);
            flights.Sort();

            string filePath = $@"{Environment.GetEnvironmentVariable("Temp")}\\seats_aero_flights_[dateStamp]_[timeStamp]";
            filePath = filePath.Replace("[dateStamp]", DateTime.Now.ToString("yyyyMMdd"));
            filePath = filePath.Replace("[timeStamp]", DateTime.Now.ToString("HHmmss"));
            FileIO.ExportJsonFile(flights, filePath + ".json");

            FileIO.SaveStringToFile(Flight.GetAsCSVString(flights), filePath + ".csv");
        }


        //[TestMethod]
        public void SaveRandomTestData()
        {
            SeatsAeroAPI seatsAeroInfo = new SeatsAeroAPI();
            seatsAeroInfo = new SeatsAeroAPI();

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
