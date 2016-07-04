using System;
using Specify.Logging;

namespace Specify.Exceptions
{
    /// <summary>
    /// The exception for errors when registering objects to the scenario container.
    /// </summary>
    public class InterfaceRegistrationException : Exception
    {
        /// <summary>
        /// The formatted exception message.
        /// </summary>
        public const string ExceptionMessageFormat = "Cannot register service {0} after SUT is created";

        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceRegistrationException"/> class.
        /// </summary>
        /// <param name="serviceThatCouldNotBeRegisteredForSomeReason">The service that could not be registered for some reason.</param>
        public InterfaceRegistrationException(Type serviceThatCouldNotBeRegisteredForSomeReason)
            : base(string.Format(ExceptionMessageFormat, serviceThatCouldNotBeRegisteredForSomeReason.FullName))
        {
            this.Log().ErrorFormat(ExceptionMessageFormat, serviceThatCouldNotBeRegisteredForSomeReason.FullName);
        }
    }
}