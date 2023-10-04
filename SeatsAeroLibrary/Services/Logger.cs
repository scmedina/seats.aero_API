using NLog.Config;
using NLog.Targets;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.EventLog;

namespace SeatsAeroLibrary.Services
{
    public class Logger : ILogger
    {
        private NLog.Logger _logger;

        public Logger(NLog.Logger logger, IConfigSettings configSettings)
        {
            _logger = logger;
            SetLogSettings(configSettings);
        }

        public virtual void SetLogSettings(IConfigSettings configSettings)
        {
            configSettings.Load();
            var config = new LoggingConfiguration();
            var fileTarget = new FileTarget("fileTarget")
            {
                FileName = Path.Combine(configSettings.OutputDirectory, "logs/seats.aero_log_${shortdate}.log"),
                Layout = "${longdate} ${level:uppercase=true} - ${message}"
            };

            config.AddRule(LogLevel.Info, LogLevel.Fatal, fileTarget);
            LogManager.Configuration = config;
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
