using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services
{
    /// <summary>
    /// The purpose of this class is to demonstrate the power of dependency inversion
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        public void Error(Exception e)
        {
            Console.WriteLine($"ERROR:");
            Console.WriteLine(e?.Message);

            if (e?.InnerException != null)
                Console.WriteLine(e.InnerException);
        }

        public void Error(Exception e, string message)
        {
            Console.WriteLine($"ERROR: {message}");
            Console.WriteLine(e?.Message);

            if (e?.InnerException != null)
                Console.WriteLine(e.InnerException);
        }

        public void Info(string message)
        {
            Console.WriteLine($"INFORMATION: {message}");
        }

        public void Warn(string message)
        {
            Console.WriteLine($"WARNING: {message}");
        }
    }
}
