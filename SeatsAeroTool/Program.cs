using Autofac;
using SeatsAeroLibrary;
using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Models.Entities;
using SeatsAeroLibrary.Models.FlightFactories;
using SeatsAeroLibrary.Models.Types;
using SeatsAeroLibrary.Services;
using SeatsAeroTool.Services;

namespace SeatsAeroTool
{
    internal static class Program
    {

        private static ILogger _logger;
        private static IMessenger _messenger;

        public static SeatsAeroAPI seatsAeroInfo;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Setup Global Exception Handlers before anything else
            Application.ThreadException += OnThreadException;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();


            using (var scope = WinFormsServiceContainer.BuildContainer().BeginLifetimeScope())
            {
                _logger = scope.Resolve<ILogger>();
                _messenger = scope.Resolve<IMessenger>();
            }

            seatsAeroInfo = new SeatsAeroAPI();

            List <IFlightFilterFactory> filterFactories = new List<IFlightFilterFactory>();
            SeatType seatTypes = SeatType.FFirstClass | SeatType.JBusiness;
            filterFactories.Add(new SeatAvailabilityFilterFactory(seatTypes, 4));
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

            Task<List<Flight>> flightsAsync = seatsAeroInfo.LoadAvailabilityAndFilter(MileageProgram.all, false, filterFactories);
            flightsAsync.Wait();
            List<Flight> flights = flightsAsync.Result;

            Application.Run(new MainForm());
        }


        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            OnExceptionExit((Exception)e.ExceptionObject);
        }

        private static void OnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            OnExceptionExit(e.Exception);
        }
        private static void OnExceptionExit(Exception e)
        {
            _logger?.Error(e);
            //_messenger?.ShowMessageBox("Oops! Something went wrong!", "Unexpected Exception");
            Environment.Exit(1);
        }
    }
}