using System;

namespace Specify
{
    /// <summary>
    /// Helper class for testing methods that will throw exceptions.
    /// </summary>
    public static class Catch
    {
        /// <summary>
        /// Perform the action and catch any exception thrown.
        /// </summary>
        /// <param name="throwingAction">The throwing action.</param>
        /// <returns>Exception.</returns>
        public static Exception Exception(Action throwingAction)
        {
            return Only<Exception>(throwingAction);
        }

        /// <summary>
        /// Perform the action and catch any exception thrown.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="throwingFunc">The throwing function.</param>
        /// <returns>Exception.</returns>
        public static Exception Exception<T>(Func<T> throwingFunc)
        {
            try
            {
                throwingFunc();
            }
            catch (Exception exception)
            {
                return exception;
            }

            return null;
        }

        /// <summary>
        /// Perform the action and catch only the specified type of exception.
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <param name="throwingAction">The throwing action.</param>
        /// <returns>TException.</returns>
        public static TException Only<TException>(Action throwingAction)
          where TException : Exception
        {
            try
            {
                throwingAction();
            }
            catch (TException exception)
            {
                return exception;
            }

            return null;
        }
    }
}
