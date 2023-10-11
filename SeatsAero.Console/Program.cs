// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using Autofac;
using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Models.Entities;
using SeatsAeroLibrary.Repositories;
using SeatsAeroLibrary.Services;
using SeatsAeroLibrary.Services.API.Factories;
using System.Data;

//string filePath = args[0];
//if (System.IO.File.Exists(filePath) == false)
//{
//    Console.WriteLine($"{filePath} does not exist.");
//    Console.ReadLine();
//    Environment.Exit(0);
//}


IConfigSettings configSettings; ILogger logger; ITripSearchRepository repository;
ITripSearchService tripSearchService; IAPIWithFiltersFactory aPIWithFiltersFactory;
IFlightRecordService flightRecordService; IDataAccess dataAccess;
using (var scope = ServicesContainer.BuildContainer().BeginLifetimeScope())
{
    configSettings = scope.Resolve<IConfigSettings>();
    logger = scope.Resolve<ILogger>();
    repository = scope.Resolve<ITripSearchRepository>();
    tripSearchService = scope.Resolve<ITripSearchService>();
    aPIWithFiltersFactory = scope.Resolve<IAPIWithFiltersFactory>();
    flightRecordService = scope.Resolve<IFlightRecordService>();
    dataAccess = scope.Resolve<IDataAccess>();
}

IEnumerable<TripSearchDataModel> searchData = repository.GetAll();
List<TripSearch> trips = tripSearchService.GetTripSearches(searchData);

//flightRecordService.AddRepositoryType<FlightRecordByDayOfWeekRepository>();
//flightRecordService.AddRepositoryType<FlightRecordByDateRepository>();
//flightRecordService.AddRepositoryType<FlightRecordBySeatTypeRepository>();
flightRecordService.AddRepositoryType<FlightRecordRepository>();

tripSearchService.GetAllFlightsFromCachedSearches(trips);

string dir = configSettings.OutputDirectory;
string[] views = { "flight_lows_csv_export_by_seattype", "flight_lows_csv_export_by_dayofweek", "flight_lows_csv_export" };
foreach (string view in views)
{
    string query= $"select * from {view}";
    string fileName = $"{dir}\\{view}.csv";
    using (DataTable dt = dataAccess.GetDataTable(query))
    {
        FileIO.ExportToCsv(dt, fileName);
    }
}

Environment.Exit(0);