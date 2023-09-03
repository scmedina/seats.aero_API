using SeatsAeroLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services
{
    public class FilterProperties : IFilterProperties
    {
        public int SeatsAvailable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime StartDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime EndDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public SeatType SeatType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DirectOnly { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int MaxPoints { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<string> StartingAirports { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<string> EndingAirports { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
