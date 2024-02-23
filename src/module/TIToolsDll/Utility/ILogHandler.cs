using System;
using System.Collections.Generic;
using System.Text;

namespace TIToolsDll.Utility
{
    public interface ILogHandler
    {
        void LogProgress(string message);
        void LogVerbose(string message);
        void LogDebug(string message);
        void LogWarning(string message);
        void LogError(Exception exception);
    }
}
