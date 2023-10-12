using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.FlightFilters
{
    public class SourceFilter : BasicFilter
    {
        public List<MileageProgram> Programs { get; set; } = new List<MileageProgram>();
        public MileageProgram SourcesEnum { get; set; } = MileageProgram.None;

        public override string ToString()
        {
            return this.GetType().Name;
        }

        public SourceFilter(string sources)
        {
            if (string.IsNullOrEmpty(sources)) { return; }

            string[] sourceArray = sources.Split(',');
            foreach (string source in sourceArray)
            {
                MileageProgram program = MileageProgram.None;
                Guard.AgainstInvalidSource(source, nameof(source), out program);
                SourcesEnum = SourcesEnum | program;
                Programs.Add(program);
            }
        }

        public SourceFilter(MileageProgram thisProgram)
        {
            SourcesEnum = thisProgram;
            EnumHelper helper = new EnumHelper();
            Programs = helper.GetBitFlagList(thisProgram);
        }

        protected override bool FilterFlight(Flight flight)
        {
            if (Programs == null || Programs.Count == 0) { return true; }
            return Programs.Contains(flight.Source);
        }
    }
}
