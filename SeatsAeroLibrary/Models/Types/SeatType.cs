using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models
{
    [Flags]
    public enum SeatType
    {
        None = 0,
        YEconomy = 1,
        W  = 2,
        JBusiness = 4,
        FFirstClass = 8,
        Any = 15
    }
}
