using System;
using System.Collections.Generic;
using Autofac;
using Specify.Configuration;
using Specify.Mocks;

namespace Specify.Autofac
{
    public class SpecifyAutofacBootstrapper : IConfigureSpecify
    {
        public IContainer ApplicationContainer { get; }
        public List<IPerAppDomainActions> PerAppDomainActions { get; }
        public List<IPerScenarioActions> PerScenarioActions { get; }
        public bool LoggingEnabled { get; set; }

        public SpecifyAutofacBootstrapper()
        {
            PerAppDomainActions = new List<IPerAppDomainActions>();
            PerScenarioActions = new List<IPerScenarioActions>();
            LoggingEnabled = false;
            ApplicationContainer = BuildContainer();
        }

        public virtual void ConfigureContainer(ContainerBuilder builder)
        {

        }

        public virtual Func<IMockFactory> GetMockFactory()
        {
            return new MockDetector().FindAvailableMock();
        } 

        private IContainer BuildContainer()
        {
            var mockFactory = GetMockFactory();
            var builder = new AutofacContainerFactory().Create(mockFactory);
            ConfigureContainer(builder);
            var container = builder.Build();
            return new AutofacContainer(container);
        }
    }
}