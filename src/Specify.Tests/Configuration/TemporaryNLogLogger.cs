using System;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Time;

namespace Specify.Tests.Configuration
{
    public class TemporaryNLogLogger : IDisposable
    {
        private LoggingConfiguration _originalNlogConfig;
        private TimeSource _originalTimeSource;

        public TemporaryNLogLogger(string filePath)
        {
            _originalNlogConfig = LogManager.Configuration;
            _originalTimeSource = TimeSource.Current;

            var fileTarget = new FileTarget();
            fileTarget.DeleteOldFileOnStartup = true;
            fileTarget.FileName = filePath;

            var config = new LoggingConfiguration();
            config.AddTarget("file", fileTarget);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, fileTarget));
            LogManager.Configuration = config;

            TimeSource.Current = new StaticNlogTimeSource();
        }
        public void Dispose()
        {
            LogManager.Configuration = _originalNlogConfig;
            TimeSource.Current = _originalTimeSource;
        }

        private class StaticNlogTimeSource : TimeSource
        {
            public override DateTime Time => new DateTime(2014, 3, 25, 11, 30, 5);
        }
    }
}