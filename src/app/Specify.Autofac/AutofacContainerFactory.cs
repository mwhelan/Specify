using System.Linq;
using Specify.lib;
using Autofac;
using Autofac.Features.ResolveAnything;
using Specify.Configuration;
using Specify.Logging;
using Specify.Mocks;

namespace Specify.Autofac
{
    internal class AutofacContainerFactory
    {
        private readonly IBootstrapSpecify _configuration;
        private readonly ContainerBuilder _builder = new ContainerBuilder();

        public AutofacContainerFactory(IBootstrapSpecify configuration)
        {
            _configuration = configuration;
        }

        public ContainerBuilder Create()
        {
            RegisterScenarios();
            RegisterScenarioContainer();
            RegisterActions();

            return _builder;
        }

        private void RegisterScenarios()
        {
            var assemblies = AssemblyTypeResolver.GetAllAssembliesFromAppDomain().ToArray();
            _builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof(ScenarioFor<>));
            _builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof(ScenarioFor<,>));
        }

        private void RegisterScenarioContainer()
        {
            if (_configuration.MockFactory.GetType() == typeof(NullMockFactory))
            {
                _builder.Register<IContainer>(c => new AutofacContainer(c.Resolve<ILifetimeScope>().BeginLifetimeScope()));
                this.Log().DebugFormat("Registered {ScenarioContainer} for IContainer", "TinyContainer");
            }
            else
            {
                _builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
                _builder.RegisterSource(new AutofacMockRegistrationHandler(_configuration.MockFactory));
                _builder.Register<IContainer>(c => new AutofacContainer(c.Resolve<ILifetimeScope>().BeginLifetimeScope()));

                this.Log().DebugFormat("Registered {ScenarioContainer} for IContainer with mock factory {MockFactory}", "TinyMockingContainer", _configuration.MockFactory.MockProviderName);
            }
        }

        private void RegisterActions()
        {
            foreach (var applicationAction in _configuration.PerAppDomainActions)
            {
                _builder.RegisterType(applicationAction.GetType()).As<IPerApplicationAction>();
            }

            foreach (var scenarioAction in _configuration.PerScenarioActions)
            {
                _builder.RegisterType(scenarioAction.GetType()).As<IPerScenarioAction>();
            }
        }
    }
}