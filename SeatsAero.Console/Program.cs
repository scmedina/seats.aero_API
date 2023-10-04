// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using Autofac;
using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Models.Entities;
using SeatsAeroLibrary.Repositories;
using SeatsAeroLibrary.Services;
using SeatsAeroLibrary.Services.API.Factories;

//string filePath = args[0];
//if (System.IO.File.Exists(filePath) == false)
//{
//    Console.WriteLine($"{filePath} does not exist.");
//    Console.ReadLine();
//    Environment.Exit(0);
//}


IConfigSettings configSettings; ILogger logger; ITripSearchRepository repository;
ITripSearchService tripSearchService; IAPIWithFiltersFactory aPIWithFiltersFactory;
using (var scope = ServicesContainer.BuildContainer().BeginLifetimeScope())
{
    configSettings = scope.Resolve<IConfigSettings>();
    logger = scope.Resolve<ILogger>();
    repository = scope.Resolve<ITripSearchRepository>();
    tripSearchService = scope.Resolve<ITripSearchService>();
    aPIWithFiltersFactory = scope.Resolve<IAPIWithFiltersFactory>();
}

IEnumerable<TripSearchDataModel> searchData = repository.GetAll();
List<TripSearch> trips = tripSearchService.GetTripSearches(searchData);

tripSearchService.GetAllFlightsFromCachedSearches(trips);

Environment.Exit(0);