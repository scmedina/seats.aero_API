using SeatsAeroLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.FlightFilters
{
    public class SourceFilter : BasicFilter
    {
        public List<MileageProgram> Programs { get; set; }

        public SourceFilter(string sources)
        {
            if (String.IsNullOrEmpty(sources)) { return; }

            string[] sourceArray = sources.Split(',');
            foreach (string source in sourceArray)
            {
                MileageProgram program = MileageProgram.None;
                Guard.AgainstInvalidSource(source, nameof(source),out program);
                Programs.Add(program);
            }
        }

        protected override bool FilterFlight(Flight flight)
        {
            return Programs.Contains(flight.Source);
        }
    }
}
