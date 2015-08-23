using System;
using Specify.Logging;

namespace Specify.Exceptions
{
    public class InterfaceResolutionException : Exception
    {
        public const string ExceptionMessageFormat = "Failed to resolve an implementation of {0}.";
        public InterfaceResolutionException(Exception innerException, Type interfaceThatCouldNotBeResolvedForSomeReason)
            : base(string.Format(ExceptionMessageFormat, interfaceThatCouldNotBeResolvedForSomeReason.FullName), innerException)
        {
            this.Log().ErrorFormat(ExceptionMessageFormat, interfaceThatCouldNotBeResolvedForSomeReason.FullName);
        }
    }
}