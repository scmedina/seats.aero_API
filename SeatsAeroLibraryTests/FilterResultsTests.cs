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
            SeatType seatTypes = SeatType.FFirstClass | SeatType.W | SeatType.JBusiness;
            filterFactories.Add(new SeatAvailabilityFilterFactory(seatTypes, 1));
            filterFactories.Add(new DirectFilterFactory(seatTypes, true));
            filterFactories.Add(new MaxMileageCostFilterFactory(seatTypes, 100000, true));
            LocationByType houstonLocation = new LocationByType("IAH", SeatsAeroLibrary.Helpers.LocationType.Airport);
            filterFactories.Add(new LocationFilterFactory(
                new List<SeatsAeroLibrary.Models.Entities.LocationByType> { houstonLocation },
                isDestination: false
                ));

            SeatsAeroAPI seatsAeroInfo = new SeatsAeroAPI();

            List<Flight> flights = seatsAeroInfo.FilterAvailability(data, filterFactories);
        }

        private List<AvailabilityDataModel> GetAvailabilities()
        {
            List<string> fileNames = new List<string>() { "seats_aero_american_20230909.json", "seats_aero_lifemiles_20230909.json" };
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
