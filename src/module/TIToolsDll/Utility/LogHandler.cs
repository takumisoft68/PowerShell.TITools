using System;
using System.Collections.Generic;
using System.Text;

namespace TIToolsDll.Utility
{
    public class LogHandler : ILogHandler
    {
        public LogHandler(
            Action<string> logProgress,
            Action<string> logVerbose,
            Action<string> logDebug,
            Action<string> logWarning,
            Action<Exception> logError)
        {
            _logProgress = logProgress;
            _logVerbose = logVerbose;
            _logDebug = logDebug;
            _logWarning = logWarning;
            _logError = logError;
        }

        private Action<string> _logProgress { get; } = null;
        private Action<string> _logVerbose { get; } = null;
        private Action<string> _logDebug { get; } = null;
        private Action<string> _logWarning { get; } = null;
        private Action<Exception> _logError { get; } = null;

        public void LogProgress(string message) => _logProgress?.Invoke(message);
        public void LogVerbose(string message) => _logVerbose?.Invoke(message);
        public void LogDebug(string message) => _logDebug?.Invoke(message);
        public void LogWarning(string message) => _logWarning?.Invoke(message);
        public void LogError(Exception exception) => _logError?.Invoke(exception);
    }
}
