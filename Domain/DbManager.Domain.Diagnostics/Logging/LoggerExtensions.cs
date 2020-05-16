using DbManager.Domain.Diagnostics.Logging.Wrappers;
using Microsoft.Extensions.Logging;

namespace DbManager.Domain.Diagnostics.Logging
{
    public static class LoggerExtensions
    {
        public static INullableLogger Wrap<T>(this ILogger<T> logger)
        {
            return new NullableLogger<T>(logger);
        }
    }
}