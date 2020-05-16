using Microsoft.Extensions.Logging;

namespace DbManager.Domain.Diagnostics.Logging.Wrappers
{
    internal class CriticalLog<T> : ILog
    {
        private readonly ILogger<T> _logger;

        public CriticalLog(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void Log(string message)
        {
            _logger.LogCritical(message);
        }

        public void Log(string message, params object[] args)
        {
            _logger.LogCritical(message, args);
        }
    }
}