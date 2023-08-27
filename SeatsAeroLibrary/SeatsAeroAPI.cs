using RestSharp;
using System.Runtime.CompilerServices;
using System.Text;

namespace SeatsAeroLibrary
{
    public class SeatsAeroAPI
    {
        public SeatsAeroAPI ()
        {
        }

        public async void LoadAvailability(MileageProgram mileageProgram, bool forceMostRecent = false)
        {

            string jsonResults = "";
            FileSnapshot fileSnapshot = new FileSnapshot();

            if (fileSnapshot.TryFindValidSnapshot(mileageProgram, ref jsonResults) == false)
            {
                jsonResults = await TryGetAPIAvailabilityResults(mileageProgram);
            }

            System.Diagnostics.Debug.WriteLine("{0}", jsonResults);
        }

        private async Task<string> TryGetAPIAvailabilityResults(MileageProgram mileageProgram)
        {
            var options = new RestClientOptions($"https://seats.aero/api/availability?source={mileageProgram.ToString()}");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            var response = await client.GetAsync(request);

            string results = response.Content == null ? "" : response.Content;

            FileSnapshot fileSnapshot = new FileSnapshot();
            string filePath = FileSnapshot.SnapshotFileDirectory + fileSnapshot.GetFileNameBySourceAndDate(mileageProgram, DateTime.Now);
            System.IO.File.WriteAllText(filePath, results);

            return results;
        }

        
    }
}