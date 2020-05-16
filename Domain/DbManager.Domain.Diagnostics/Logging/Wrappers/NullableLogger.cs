using System;
using Microsoft.Extensions.Logging;

namespace DbManager.Domain.Diagnostics.Logging.Wrappers
{
    internal class NullableLogger<T> : INullableLogger
    {
        private readonly ILogger<T> _logger;
        private readonly ILog _trace;
        private readonly ILog _debug;
        private readonly ILog _info;
        private readonly ILog _warn;
        private readonly ILog _error;
        private readonly ILog _critical;

        public NullableLogger(ILogger<T> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _trace = logger.IsEnabled(LogLevel.Trace) ? new TraceLog<T>(logger) : null;
            _debug = logger.IsEnabled(LogLevel.Debug) ? new DebugLog<T>(logger) : null;
            _info = logger.IsEnabled(LogLevel.Information) ? new InfoLog<T>(logger) : null;
            _warn = logger.IsEnabled(LogLevel.Warning) ? new WarnLog<T>(logger) : null;
            _error = logger.IsEnabled(LogLevel.Error) ? new ErrorLog<T>(logger) : null;
            _critical = logger.IsEnabled(LogLevel.Critical) ? new CriticalLog<T>(logger) : null;
        }

        public ILog Trace => _trace;

        public ILog Debug => _debug;

        public ILog Info => _info;

        public ILog Warn => _warn;

        public ILog Error => _error;

        public ILog Critical => _critical;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            _logger.Log(logLevel, eventId, state, exception, formatter);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _logger.IsEnabled(logLevel);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return _logger.BeginScope(state);
        }
    }
}