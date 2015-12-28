using System;
using Specify.Logging;

namespace Specify.Exceptions
{
    /// <summary>
    /// The exception for errors when resolving objects from the scenario container.
    /// </summary>
    public class InterfaceResolutionException : Exception
    {
        /// <summary>
        /// The formatted exception message.
        /// </summary>
        public const string ExceptionMessageFormat = "Failed to resolve an implementation of {0}.";

        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceResolutionException"/> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="interfaceThatCouldNotBeResolvedForSomeReason">The interface that could not be resolved for some reason.</param>
        public InterfaceResolutionException(Exception innerException, Type interfaceThatCouldNotBeResolvedForSomeReason)
            : base(string.Format(ExceptionMessageFormat, interfaceThatCouldNotBeResolvedForSomeReason.FullName), innerException)
        {
            this.Log().ErrorFormat(ExceptionMessageFormat, interfaceThatCouldNotBeResolvedForSomeReason.FullName);
        }
    }
}