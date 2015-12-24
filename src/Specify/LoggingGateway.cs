using System;
using System.Collections.Concurrent;
using Specify.Exceptions;
using Specify.Logging;

namespace Specify
{
    /// <summary>
    /// Static gateway to LibLog loggers
    /// </summary>
    internal static class LoggingGateway
    {
        /// <summary>
        /// Concurrent dictionary that ensures only one instance of a logger for a type.
        /// </summary>
        private static readonly ConcurrentDictionary<string, ILog> Dictionary = new ConcurrentDictionary<string, ILog>();

        /// <summary>
        /// Gets the logger for <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type to get the logger for.</param>
        /// <returns>Instance of a logger for the object.</returns>
        internal static ILog Log<T>(this T type)
        {
            var objectName = typeof (T).FullName;
            return Log(objectName);
        }

        /// <summary>
        /// Gets the logger for the specified object name.
        /// </summary>
        /// <param name="objectName">Either use the fully qualified object name or the short. If used with Log&lt;T&gt;() you must use the fully qualified object name"/></param>
        /// <returns>Instance of a logger for the object.</returns>
        internal static ILog Log(this string objectName)
        {
            try
            {
                return Dictionary.GetOrAdd(objectName, LogProvider.GetLogger);
            }
            catch (Exception ex)
            {
                throw new LoggingException(ex, objectName);
            }
        }
    }
}