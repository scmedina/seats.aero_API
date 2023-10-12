using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services
{
    public interface ILogger
    {
        void Error(Exception e);
        void Error(Exception e, string message);
        void Info(string message);
        void Warn(string message);
    }
}
