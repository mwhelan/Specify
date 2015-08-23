using System;
using Specify.Logging;

namespace Specify.Exceptions
{
    public class InterfaceRegistrationException : Exception
    {
        public const string ExceptionMessageFormat = "Cannot register service {0} after SUT is created";
        public InterfaceRegistrationException(Type serviceThatCouldNotBeRegisteredForSomeReason)
            : base(string.Format(ExceptionMessageFormat, serviceThatCouldNotBeRegisteredForSomeReason.FullName))
        {
            this.Log().ErrorFormat(ExceptionMessageFormat, serviceThatCouldNotBeRegisteredForSomeReason.FullName);
        }
    }
}