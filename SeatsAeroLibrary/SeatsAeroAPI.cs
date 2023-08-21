using RestSharp;

namespace SeatsAeroLibrary
{
    public class SeatsAeroAPI
    {
        public SeatsAeroAPI ()
        {
        }

        public async void LoadAvailability(MileageProgram mileageProgram)
        {

            var options = new RestClientOptions($"https://seats.aero/api/availability?source={mileageProgram.ToString()}");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            var response = await client.GetAsync(request);

            System.Diagnostics.Debug.WriteLine("{0}", response.Content);
        }
    }
}