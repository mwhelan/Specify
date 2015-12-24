using System;
using System.Collections.Generic;
using Specify.Mocks;
using TinyIoC;

namespace Specify.Configuration
{
    public class SpecifyBootstrapper : IConfigureSpecify
    {
        public IContainer ApplicationContainer { get; set; }
        public List<IPerAppDomainActions> PerAppDomainActions { get; private set; }
        public List<IPerScenarioActions> PerScenarioActions { get; private set; }

        public bool LoggingEnabled { get; set; }

        public SpecifyBootstrapper()
        {
            PerAppDomainActions = new List<IPerAppDomainActions>();
            PerScenarioActions = new List<IPerScenarioActions>();
            LoggingEnabled = false;
            ApplicationContainer = BuildContainer();
        }

        public virtual void ConfigureContainer(TinyIoCContainer container)
        {
            
        }

        public virtual Func<IMockFactory> GetMockFactory()
        {
            return new MockDetector().FindAvailableMock();
        }

        private IContainer BuildContainer()
        {
            var mockFactory = GetMockFactory();
            var container = new TinyContainerFactory().Create(mockFactory);
            ConfigureContainer(container);
            return new TinyContainer(container);
        }
    }
}
