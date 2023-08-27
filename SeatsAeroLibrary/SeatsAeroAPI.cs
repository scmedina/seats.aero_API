using RestSharp;
using SeatsAeroLibrary.Models;
using System.Runtime.CompilerServices;
using System.Text;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace SeatsAeroLibrary
{
    public class SeatsAeroAPI
    {
        public SeatsAeroAPI ()
        {
        }

        public async void LoadAvailability(MileageProgram mileageProgram, bool forceMostRecent = false)
        {
            for (int i = 0; i<12; i++)
            {
                MileageProgram thisProgram = (MileageProgram)Math.Pow(2, i);
                if (mileageProgram.HasFlag(thisProgram))
                {
                    LoadAvailabilitySingle(thisProgram, forceMostRecent);
                }
            }
        }
        public async void LoadAvailabilitySingle(MileageProgram mileageProgram, bool forceMostRecent = false)
        {

            MileageProgramHelpers.CheckForSingleMileageProgram(mileageProgram);

            string json = "";
            FileSnapshot fileSnapshot = new FileSnapshot();

            if (forceMostRecent == true ||  fileSnapshot.TryFindValidSnapshot(mileageProgram, ref json) == false)
            {
                json = await TryGetAPIAvailabilityResults(mileageProgram);
            }

            List<Availability> availabilities = JsonSerializer.Deserialize<List<Availability>>(json);

            //System.Diagnostics.Debug.WriteLine("{0}", jsonResults);
        }

        private async Task<string> TryGetAPIAvailabilityResults(MileageProgram mileageProgram)
        {
            MileageProgramHelpers.CheckForSingleMileageProgram(mileageProgram);

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