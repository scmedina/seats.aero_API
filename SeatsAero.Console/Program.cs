// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Models.Entities;
using SeatsAeroLibrary.Repositories;

string filePath = args[0];
if (System.IO.File.Exists(filePath) == false)
{
    Console.WriteLine($"{filePath} does not exist.");
    Console.ReadLine();
    Environment.Exit(0);
}

TripSearchRepository repository = new TripSearchRepository(filePath);
IEnumerable<TripSearchDataModel> searchData = repository.GetAll();
List<TripSearch> trips = TripSearch.GetTripSearches(searchData);

TripSearch.GetAllFlightsFromCachedSearches(trips);

Environment.Exit(0);