using SeatsAeroLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.Sort
{
    public class FlightSorter : BasicSorter<Flight, FlightSortFields>
    {
        protected override object GetFieldValue(FlightSortFields field, Flight flight)
        {
            switch (field)
            {
                case FlightSortFields.MileageCost:
                    return flight.MileageCost;
                case FlightSortFields.DepartureTime:
                    return flight.Date;
                default:
                    throw new ArgumentException("Invalid sort field.");
            }
        }
    }
}
