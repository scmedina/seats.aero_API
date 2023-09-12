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
    public class DebuggerLogger : ILogger
    {
        public void Error(Exception e)
        {
            System.Diagnostics.Debug.WriteLine($"ERROR:");
            System.Diagnostics.Debug.WriteLine(e?.Message);

            if (e?.InnerException != null)
                System.Diagnostics.Debug.WriteLine(e.InnerException);
        }

        public void Error(Exception e, string message)
        {
            System.Diagnostics.Debug.WriteLine($"ERROR: {message}");
            System.Diagnostics.Debug.WriteLine(e?.Message);

            if (e?.InnerException != null)
                System.Diagnostics.Debug.WriteLine(e.InnerException);
        }

        public void Info(string message)
        {
            System.Diagnostics.Debug.WriteLine($"INFORMATION: {message}");
        }

        public void Warn(string message)
        {
            System.Diagnostics.Debug.WriteLine($"WARNING: {message}");
        }
    }
}
