using System;
using System.Text;
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
            : base(CreateErrorMessage(interfaceThatCouldNotBeResolvedForSomeReason.FullName), innerException)
        {
            this.Log().ErrorFormat(ExceptionMessageFormat, interfaceThatCouldNotBeResolvedForSomeReason.FullName);
        }

        private static string CreateErrorMessage(string interfaceName)
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format(ExceptionMessageFormat, interfaceName));
            sb.AppendLine("There are 3 ways to resolve this exception:");
            sb.AppendLine(
                "1. In the Scenario class, set an implementation of the interface before the SUT property is called the first time.");
            sb.AppendLine(
                "2. If the Container is an IoC container, then register an implementation for this interface in the container");
            sb.AppendLine("3. If you want to use automocking, then register a mocking framework in the test project.");
            sb.AppendLine("Specify is currently configured with these settings:");
            sb.AppendLine($"- Bootstrapper: {Host.Configuration.GetType().Name}");
            sb.AppendLine($"- Application Container: {Host.Configuration.ApplicationContainer.GetType().Name}");
            sb.AppendLine($"- Mock Provider: {Host.Configuration.MockFactory.MockProviderName}");

            return sb.ToString();
        }
    }
}