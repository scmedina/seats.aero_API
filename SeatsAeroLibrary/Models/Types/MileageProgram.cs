using SeatsAeroLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models
{
    //Source	Mileage Program	Supported Cabins	Has Seat Count	Has Trip Data
    [Flags]
    public enum MileageProgram
    {
        None = 0,
        // lifemiles  Avianca LifeMiles   Y/J/F   Yes Yes
        [SourceDetailsAttribute(hasSeatCount:true, hasTripData: true)]
        lifemiles = 1,
        // virginatlantic  Virgin Atlantic Flying Club Y/W/J   Yes Yes
        [SourceDetailsAttribute(hasSeatCount: true, hasTripData: true)]
        virginatlantic = 2,
        // aeromexico  Aeromexico Club Premier Y/W/J   Yes No,
        [SourceDetailsAttribute(hasSeatCount: true, hasTripData: false)]
        aeromexico = 4,
        // american    American Airlines   Y/W/J/F No  Yes
        [SourceDetailsAttribute(hasSeatCount: false, hasTripData: true)]
        american = 8,
        // delta   Delta SkyMiles  Y/W/J   Yes Yes
        [SourceDetailsAttribute(hasSeatCount: true, hasTripData: true)]
        delta = 16,
        // etihad  Etihad Guest    Y/J Yes No
        [SourceDetailsAttribute(hasSeatCount: true, hasTripData: false)]
        etihad = 32,
        // united  United MileagePlus  Y/W/J/F Yes Yes
        [SourceDetailsAttribute(hasSeatCount: true, hasTripData: true)]
        united = 64,
        // emirates    Emirates Skywards   Y/W/J/F No  No
        [SourceDetailsAttribute(hasSeatCount: false, hasTripData: false)]
        emirates = 128,
        // aeroplan    Air Canada Aeroplan Y/W/J/F No  Yes
        [SourceDetailsAttribute(hasSeatCount: false, hasTripData: true)]
        aeroplan = 256,
        // alaska  Alaska Mileage Plan Y/W/J/F Yes Yes
        [SourceDetailsAttribute(hasSeatCount: true, hasTripData: true)]
        alaska = 512,
        // velocity    Virgin Australia Velocity   Y/W/J/F Yes Yes
        [SourceDetailsAttribute(hasSeatCount: true, hasTripData: true)]
        velocity = 1024,
        // qantas  Qantas Frequent Flyer   Y/W/J/F No  Yes
        [SourceDetailsAttribute(hasSeatCount: false, hasTripData: true)]
        qantas = 2048,
        // flyingblue    Air France / KLM Flying Blue   Y/W/J/F Yes  Yes
        [SourceDetailsAttribute(hasSeatCount: true, hasTripData: true)]
        flyingblue = 4096,
        all = 8191
    }


    public class SingleMileageProgramRequiredException : InvalidOperationException
    {
        public SingleMileageProgramRequiredException(string message) : base(message)
        {

        }
    }
}
