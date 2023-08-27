using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary
{
    //Source	Mileage Program	Supported Cabins	Has Seat Count	Has Trip Data
    [FlagsAttribute]
    public enum MileageProgram
    {
        None = 0,
        // lifemiles  Avianca LifeMiles   Y/J/F   Yes Yes
        lifemiles = 1,
        // virginatlantic  Virgin Atlantic Flying Club Y/W/J   Yes Yes
        virginatlantic = 2,
        // aeromexico  Aeromexico Club Premier Y/W/J   Yes No,
        aeromexico = 4,
        // american    American Airlines   Y/W/J/F No  Yes
        american = 8,
        // delta   Delta SkyMiles  Y/W/J   Yes Yes
        delta = 16,
        // etihad  Etihad Guest    Y/J Yes No
        etihad = 32,
        // united  United MileagePlus  Y/W/J/F Yes Yes
        united = 64,
        // emirates    Emirates Skywards   Y/W/J/F No  No
        emirates = 128,
        // aeroplan    Air Canada Aeroplan Y/W/J/F No  Yes
        aeroplan =  256,
        // alaska  Alaska Mileage Plan Y/W/J/F Yes Yes
        alaska = 512,
        // velocity    Virgin Australia Velocity   Y/W/J/F Yes Yes
        velocity=1024,
        // qantas  Qantas Frequent Flyer   Y/W/J/F No  Yes
        qantas = 2048,
        all = 4095
    }

}
