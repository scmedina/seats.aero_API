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
        /// <summary>
        /// Y, Economy
        /// </summary>
        [Description("Y")]
        Economy = 1,
        /// <summary>
        /// W, Premium Economy
        /// </summary>
        [Description("W")]
        PremiumEconomy  = 2,
        /// <summary>
        /// J, Business
        /// </summary>
        [Description("J")]
        Business = 4,
        /// <summary>
        /// F, FirstClass
        /// </summary>
        [Description("F")]
        First = 8,
        Any = 15
    }
}
