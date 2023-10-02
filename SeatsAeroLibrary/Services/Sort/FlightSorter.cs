using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.Sort
{
    public class FlightSorter : BasicSorter<Flight>
    {
        public override List<Enum> GetFieldsList(string sortFields)
        {
            EnumHelper enumHelper = new EnumHelper();
            return (List<Enum>)enumHelper.GetEnumList<FlightSortFields>(sortFields).Select(val => (Enum) val).ToList();
        }

        protected override object GetFieldValue(Enum field, Flight flight)
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
