using System;
using System.Linq;
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
            var container = new TinyIoCContainer();
            RegisterScenarios(container);
            RegisterScenarioContainer(container, mockFactory);
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
                container.Register<IContainer>((c, p) => new TinyContainer(c.GetChildContainer()));
                this.Log()
                    .DebugFormat("Registered {ScenarioContainer} for IContainer", "TinyContainer");
            }
            else
            {
                container.Register<IContainer>((c, p) => new TinyMockingContainer(mockFactory, c.GetChildContainer()));
                this.Log()
                    .DebugFormat("Registered {ScenarioContainer} for IContainer with mock factory {MockFactory}", "TinyMockingContainer", mockFactory.MockProviderName);
            }
        }
    }
}