using System;
using Microsoft.Extensions.Logging;

namespace DbManager.Domain.Diagnostics.Logging
{
    internal class NullableLogger : INullableLogger
    {
        private readonly ILogger _logger;
        private readonly LogWithLevel? _trace;
        private readonly LogWithLevel? _debug;
        private readonly LogWithLevel? _info;
        private readonly LogWithLevel? _warn;
        private readonly LogWithLevel? _error;
        private readonly LogWithLevel? _critical;

        public NullableLogger(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _trace = logger.Trace();
            _debug = logger.Debug();
            _info = logger.Info();
            _warn = logger.Warn();
            _error = logger.Error();
            _critical = logger.Critical();
        }

        public LogWithLevel? Trace => _trace;

        public LogWithLevel? Debug => _debug;

        public LogWithLevel? Info => _info;

        public LogWithLevel? Warn => _warn;

        public LogWithLevel? Error => _error;

        public LogWithLevel? Critical => _critical;

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