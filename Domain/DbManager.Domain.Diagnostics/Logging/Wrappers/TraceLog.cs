using Microsoft.Extensions.Logging;

namespace DbManager.Domain.Diagnostics.Logging.Wrappers
{
    internal class TraceLog<T> : ILog
    {
        private readonly ILogger<T> _logger;

        public TraceLog(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void Log(string message)
        {
            _logger.LogTrace(message);
        }

        public void Log(string message, params object[] args)
        {
            _logger.LogTrace(message, args);
        }
    }
}