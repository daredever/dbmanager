using Microsoft.Extensions.Logging;

namespace DbManager.Domain.Diagnostics.Logging
{
    public static class LoggerExtensions
    {
        public static INullableLogger Wrap(this ILogger logger)
        {
            return new NullableLogger(logger);
        }

        public static LogWithLevel? Trace(this ILogger logger) =>
            LogWithLevel.CreateIfEnabled(logger, LogLevel.Trace);

        public static LogWithLevel? Debug(this ILogger logger) =>
            LogWithLevel.CreateIfEnabled(logger, LogLevel.Debug);

        public static LogWithLevel? Info(this ILogger logger) =>
            LogWithLevel.CreateIfEnabled(logger, LogLevel.Information);

        public static LogWithLevel? Error(this ILogger logger) =>
            LogWithLevel.CreateIfEnabled(logger, LogLevel.Error);

        public static LogWithLevel? Warn(this ILogger logger) =>
            LogWithLevel.CreateIfEnabled(logger, LogLevel.Warning);

        public static LogWithLevel? Critical(this ILogger logger) =>
            LogWithLevel.CreateIfEnabled(logger, LogLevel.Critical);
    }
}