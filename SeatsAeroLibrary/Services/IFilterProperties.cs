using SeatsAeroLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services
{
    internal interface IFilterProperties
    {
        int SeatsAvailable { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        SeatType SeatType { get; set; }
        bool DirectOnly { get; set; }
        int MaxPoints { get; set; }
        List<string> StartingAirports { get; set; }
        List<string> EndingAirports { get; set; }

    }
}
