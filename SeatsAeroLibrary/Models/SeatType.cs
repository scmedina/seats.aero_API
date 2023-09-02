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
        Y = 1,
        W  = 2,
        J = 4,
        F = 8,
        Any = 15
    }
}
