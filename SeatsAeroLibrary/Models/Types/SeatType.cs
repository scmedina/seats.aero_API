using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Models
{
    [Flags]
    public enum SeatType
    {
        None = 0,
        [Description("Y")]
        YEconomy = 1,
        [Description("W")]
        WPremiumEconomy  = 2,
        [Description("J")]
        JBusiness = 4,
        [Description("F")]
        FFirstClass = 8,
        Any = 15
    }
}
