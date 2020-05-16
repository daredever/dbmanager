using System;

namespace DbManager.Domain.Diagnostics.Logging
{
    public interface ILog
    {
        void Log(string message);
        void Log(string message, params object[] args);
    }
}