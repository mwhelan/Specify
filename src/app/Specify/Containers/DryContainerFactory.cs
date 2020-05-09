using System.Linq;
using DryIoc;
using Specify.lib;
using Specify.Logging;
using Specify.Mocks;

namespace Specify.Containers
{
    internal class DryContainerFactory
    {
        public Container Create(IMockFactory mockFactory)
        {
            if (mockFactory == null)
            {
                mockFactory = new NullMockFactory();
            }

            var container = new Container(rules => rules
                .WithConcreteTypeDynamicRegistrations()
                .WithoutThrowOnRegisteringDisposableTransient()
                .With(FactoryMethod.ConstructorWithResolvableArguments));// handle multiple constructors

            RegisterScenarios(container);
            RegisterScenarioContainer(container, mockFactory);
            return container;
        }

        private DryIoc.IContainer CreateChildContainer(Container parent)
        {
            var child = parent.With(
                parent.Rules,
                parent.ScopeContext,
                RegistrySharing.CloneAndDropCache,
                parent.SingletonScope.Clone());
            return child;
        }

        private void RegisterScenarios(Container container)
        {
            var scenarioTypes = AssemblyTypeResolver
                .GetAllTypesFromAppDomain()
                .Where(type => type.IsScenario())
                .ToList();

            scenarioTypes.Each(scenario => 
                container.Register(typeof(IScenario), scenario, Reuse.Scoped));

            this.Log().DebugFormat("Registered {RegisteredScenarioCount} Scenarios", scenarioTypes.Count);
        }

        private void RegisterScenarioContainer(Container container, IMockFactory mockFactory)
        {
            if (mockFactory.GetType() == typeof(NullMockFactory))
            {
                container.RegisterDelegate<IContainer>(r => 
                    new DryContainer(CreateChildContainer(container)));
                
                this.Log()
                    .DebugFormat("Registered {ScenarioContainer} for IContainer", "DryContainer");
            }
            else
            {
                container.RegisterDelegate<IContainer>(r =>
                    new DryMockingContainer(mockFactory, CreateChildContainer(container)));
                
                this.Log()
                    .DebugFormat("Registered {ScenarioContainer} for IContainer with mock factory {MockFactory}", "DryMockingContainer", mockFactory.MockProviderName);
            }
        }
    }
}
