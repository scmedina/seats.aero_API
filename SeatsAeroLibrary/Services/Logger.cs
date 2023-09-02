using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services
{
    public class Logger : ILogger
    {
        private NLog.Logger _logger;

        public Logger(NLog.Logger logger)
        {
            _logger = logger;
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Error(Exception e, string message)
        {
            _logger.Error(e, message);

            if (e?.InnerException != null)
                _logger.Error(e.InnerException);
        }

        public void Error(Exception e)
        {
            _logger.Error(e);

            if (e?.InnerException != null)
                _logger.Error(e.InnerException);
        }
    }
}
