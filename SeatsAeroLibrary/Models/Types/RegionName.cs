using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models.Types
{
    public enum RegionName
    {
        None = 0,
        [Description("North America")]
        NorthAmerica = 1,
        [Description("South America")]
        SouthAmerica = 2,
        Europe = 3,
        Africa = 4,
        Asia = 5,
        Oceania = 6
    }
}
