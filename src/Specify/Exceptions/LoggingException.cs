using System;
using Specify.Logging;

namespace Specify.Exceptions
{
    public class LoggingException : Exception
    {
        public const string ExceptionMessageFormat = "Failed to log for {0} logger.";
        public LoggingException(Exception innerException, string loggerThatCouldNotLogForSomeReason)
            : base(string.Format(ExceptionMessageFormat, loggerThatCouldNotLogForSomeReason), innerException)
        {
        }
    }
}