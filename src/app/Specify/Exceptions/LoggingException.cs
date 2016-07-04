using System;

namespace Specify.Exceptions
{
    /// <summary>
    /// The exception for errors during logging.
    /// </summary>
    public class LoggingException : Exception
    {
        /// <summary>
        /// The formatted exception message.
        /// </summary>
        public const string ExceptionMessageFormat = "Failed to log for {0} logger.";
        
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingException"/> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="loggerThatCouldNotLogForSomeReason">The logger that could not log for some reason.</param>
        public LoggingException(Exception innerException, string loggerThatCouldNotLogForSomeReason)
            : base(string.Format(ExceptionMessageFormat, loggerThatCouldNotLogForSomeReason), innerException)
        {
        }
    }
}