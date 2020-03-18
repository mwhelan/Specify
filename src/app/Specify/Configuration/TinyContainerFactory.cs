using System;
using System.Linq;
using Specify.Configuration.Examples;
using Specify.Containers;
using Specify.lib;
using Specify.Logging;
using Specify.Mocks;
using TinyIoC;

namespace Specify.Configuration
{
    internal class TinyContainerFactory
    {
        public TinyIoCContainer Create(IMockFactory mockFactory)
        {
            if (mockFactory == null)
            {
                mockFactory = new NullMockFactory();
            }

            var container = new TinyIoCContainer();
            RegisterScenarios(container);
            RegisterScenarioContainer(container, mockFactory);
            RegisterTypes(container);
            return container;
        }

        private void RegisterScenarios(TinyIoCContainer container)
        {
            var scenarios = AssemblyTypeResolver
                .GetAllTypesFromAppDomain()
                .Where(type => type.IsScenario())
                .ToList();

            container.RegisterMultiple<IScenario>(scenarios);
            this.Log().DebugFormat("Registered {RegisteredScenarioCount} Scenarios", scenarios.Count);
        }

        private void RegisterScenarioContainer(TinyIoCContainer container, IMockFactory mockFactory)
        {
            if (mockFactory.GetType() == typeof(NullMockFactory))
            {
                container.Register<IContainerRoot>((c, p) => new TinyContainer(c));
                this.Log()
                    .DebugFormat("Registered {ScenarioContainer} for IContainer with no mock factory", "TinyContainer");
            }
            else
            {
                container.Register<IContainerRoot>((c, p) => new TinyMockingContainer(mockFactory, c));
                this.Log()
                    .DebugFormat("Registered {ScenarioContainer} for IContainer with mock factory {MockFactory}", "TinyMockingContainer", mockFactory.MockProviderName);
            }
        }

        private void RegisterTypes(TinyIoCContainer container)
        {
            container.Register<TestScope>();
            container.Register<IChildContainerBuilder, TinyChildContainerBuilder>();
        }
    }
}