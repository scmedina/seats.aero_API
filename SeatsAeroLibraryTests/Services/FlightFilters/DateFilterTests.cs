using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog.Filters;
using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Services.FlightFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services.FlightFilters.Tests
{
    [TestClass()]
    public class DateFilterTests
    {
        [TestMethod()]
        public void DateFilterTest()
        {
            DateTime date = DateTime.Now;
            DateFilter filter = new DateFilter(date);
            Assert.IsNotNull(filter);

            Flight flightLessThan = new Flight();
            flightLessThan.Date = date.AddMinutes(-1);

            Flight flightMoreThan = new Flight();
            flightMoreThan.Date = date.AddMinutes(1);

            Flight flightSame = new Flight();
            flightSame.Date = date;

            List<Flight> flights = new List<Flight>(){ flightLessThan, flightMoreThan, flightSame};

            List<Flight> filteredFlights = filter.Filter(flights);

            Assert.AreEqual(2, filteredFlights.Count);
            Assert.AreEqual(flightMoreThan, filteredFlights[0]);
            Assert.AreEqual(flightSame, filteredFlights[1]);
            
        }

        [TestMethod()]
        public void GetDateValTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetDateValTest1()
        {
            Assert.Fail();
        }
    }
}