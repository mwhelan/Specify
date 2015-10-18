using System;
using System.Collections.Generic;
using Specify.Configuration.Mocking;
using Specify.Mocks;

namespace Specify.Configuration
{
    public class SpecifyBootstrapper
    {
        public List<IPerAppDomainActions> PerAppDomainActions { get; private set; }
        public List<IPerScenarioActions> PerScenarioActions { get; private set; }

        public bool LoggingEnabled { get; set; }

        public SpecifyBootstrapper()
        {
            PerAppDomainActions = new List<IPerAppDomainActions>();
            PerScenarioActions = new List<IPerScenarioActions>();
            LoggingEnabled = false;
        }

        public virtual IApplicationContainer CreateApplicationContainer()
        {
            return new DefaultApplicationContainer();
        }

        /// <summary>
        /// Looks to see if these mocking frameworks are referenced by the test assembly.
        /// In order, NSubstitute, FakeItEasy, Moq.
        /// Can override this to return null if you don't want the found framework to be used, or to choose a different one if multiple found.
        /// </summary>
        /// <returns>A factory to create the found mock, or null if no mocking framework found.</returns>
        public virtual Func<IMockFactory> FindAvailableMock()
        {
            return new MockDetector().FindAvailableMock();
        }
    }
}
