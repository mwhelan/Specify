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
        private readonly IBootstrapSpecify _configuration;
        private readonly TinyIoCContainer _container = new TinyIoCContainer();

        public TinyContainerFactory(IBootstrapSpecify configuration)
        {
            _configuration = configuration;
        }

        public TinyIoCContainer Create()
        {
            RegisterScenarios();
            RegisterScenarioContainer();
            return _container;
        }

        private void RegisterScenarios()
        {
            var scenarios = AssemblyTypeResolver
                .GetAllTypesFromAppDomain()
                .Where(type => type.IsScenario())
                .ToList();

            _container.RegisterMultiple<IScenario>(scenarios);
            this.Log().DebugFormat("Registered {RegisteredScenarioCount} Scenarios", scenarios.Count);
        }

        private void RegisterScenarioContainer()
        {
            if (_configuration.MockFactory.GetType() == typeof(NullMockFactory))
            {
                _container.Register<IContainer>((c, p) => new TinyContainer(c.GetChildContainer()));
                this.Log()
                    .DebugFormat("Registered {ScenarioContainer} for IContainer", "TinyContainer");
            }
            else
            {
                _container.Register<IContainer>((c, p) => new TinyMockingContainer(_configuration.MockFactory, c.GetChildContainer()));
                this.Log()
                    .DebugFormat("Registered {ScenarioContainer} for IContainer with mock factory {MockFactory}", "TinyMockingContainer", _configuration.MockFactory.MockProviderName);
            }
        }
    }
}