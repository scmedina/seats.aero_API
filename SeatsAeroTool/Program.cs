using Autofac;
using SeatsAeroLibrary;
using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Models.Entities;
using SeatsAeroLibrary.Models.Types;
using SeatsAeroLibrary.Repositories;
using SeatsAeroLibrary.Services;
using SeatsAeroLibrary.Services.API.Factories;
using SeatsAeroLibrary.Services.FlightFactories;
using SeatsAeroTool.Services;

namespace SeatsAeroTool
{
    internal static class Program
    {

        private static ILogger _logger;
        private static IMessenger _messenger;
        private static IConfigSettings _configSettings;
        private static IStatisticsRepository _statisticsRepository;
        private static IAPIWithFiltersFactory _aPIWithFiltersFactory;

        public static SeatsAeroHelper seatsAeroInfo;
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
                _configSettings = scope.Resolve<IConfigSettings>();
                _statisticsRepository = scope.Resolve<IStatisticsRepository>();
                _aPIWithFiltersFactory = scope.Resolve<IAPIWithFiltersFactory>();
            }

            seatsAeroInfo = new SeatsAeroHelper(_logger, _configSettings, _messenger, _statisticsRepository, _aPIWithFiltersFactory);

            List <IFlightFilterFactory> filterFactories = new List<IFlightFilterFactory>();
            SeatType seatTypes = SeatType.First | SeatType.Business;
            filterFactories.Add(new SeatAvailabilityFilterFactory(4));
            filterFactories.Add(new DirectFilterFactory(true));
            filterFactories.Add(new MaxMileageCostFilterFactory(100000, true));
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

            Task<List<Flight>> flightsAsync = seatsAeroInfo.LoadAvailabilityAndFilter(MileageProgram.all, false, new List<List<IFlightFilterFactory>> { filterFactories });
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