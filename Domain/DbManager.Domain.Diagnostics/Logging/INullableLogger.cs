using Microsoft.Extensions.Logging;

namespace DbManager.Domain.Diagnostics.Logging
{
    public interface INullableLogger : ILogger
    {
        LogWithLevel? Trace { get; }
        LogWithLevel? Debug { get; }
        LogWithLevel? Info { get; }
        LogWithLevel? Warn { get; }
        LogWithLevel? Error { get; }
        LogWithLevel? Critical { get; }
    }
}