using Microsoft.Extensions.Logging;

namespace DbManager.Domain.Diagnostics.Logging.Wrappers
{
    internal class ErrorLog<T> : ILog
    {
        private readonly ILogger<T> _logger;

        public ErrorLog(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void Log(string message)
        {
            _logger.LogError(message);
        }

        public void Log(string message, params object[] args)
        {
            _logger.LogError(message, args);
        }
    }
}