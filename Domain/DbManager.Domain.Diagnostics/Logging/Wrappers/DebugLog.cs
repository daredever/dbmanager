using Microsoft.Extensions.Logging;

namespace DbManager.Domain.Diagnostics.Logging.Wrappers
{
    internal class DebugLog<T> : ILog
    {
        private readonly ILogger<T> _logger;

        public DebugLog(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void Log(string message)
        {
            _logger.LogDebug(message);
        }

        public void Log(string message, params object[] args)
        {
            _logger.LogDebug(message, args);
        }
    }
}