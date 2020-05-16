using Microsoft.Extensions.Logging;

namespace DbManager.Domain.Diagnostics.Logging.Wrappers
{
    internal class WarnLog<T> : ILog
    {
        private readonly ILogger<T> _logger;

        public WarnLog(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void Log(string message)
        {
            _logger.LogWarning(message);
        }

        public void Log(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }
    }
}