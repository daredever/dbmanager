using Microsoft.Extensions.Logging;

namespace DbManager.Domain.Diagnostics.Logging
{
    public readonly struct LogWithLevel
    {
        private readonly ILogger _logger;
        private readonly LogLevel _logLevel;

        private LogWithLevel(ILogger logger, LogLevel logLevel)
        {
            _logger = logger;
            _logLevel = logLevel;
        }

        public void Log(string message) => _logger.Log(_logLevel, message);

        public void Log(string message, params object[] args) => _logger.Log(_logLevel, message, args);

        public static LogWithLevel? CreateIfEnabled(ILogger logger, LogLevel logLevel) =>
            logger.IsEnabled(logLevel) ? new LogWithLevel(logger, logLevel) : (LogWithLevel?) null;
    }
}