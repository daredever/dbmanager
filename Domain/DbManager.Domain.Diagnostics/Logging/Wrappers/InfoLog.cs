using Microsoft.Extensions.Logging;

namespace DbManager.Domain.Diagnostics.Logging.Wrappers
{
    internal class InfoLog<T> : ILog
    {
        private readonly ILogger<T> _logger;

        public InfoLog(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void Log(string message)
        {
            _logger.LogInformation(message);
        }

        public void Log(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }
    }
}