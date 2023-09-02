using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Helpers
{
    public static class Guard
    {
        public static void AgainstMultipleSources(MileageProgram program, string argumentName)
        {

            if (MileageProgramHelpers.IsSingleAirline(program) == false)
            {
                throw new SingleMileageProgramRequiredException(program.ToString());
            }
        }
    }
}
