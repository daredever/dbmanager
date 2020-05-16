using System;
using Microsoft.Extensions.Logging;

namespace DbManager.Domain.Diagnostics.Logging
{
    public interface INullableLogger : ILogger
    {
        ILog Trace { get; }
        ILog Debug { get; }
        ILog Info { get; }
        ILog Warn { get; }
        ILog Error { get; }
        ILog Critical { get; }
    }
}