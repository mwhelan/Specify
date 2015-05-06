using Autofac;
using Autofac.Features.ResolveAnything;
using Specify.Mocks;

namespace Specify.Autofac
{
    /// <summary>
    /// Automocking container that uses mock factory to create mocks and Autofac as the container. 
    /// </summary>
    public class AutofacAutoMockingContainer : AutofacScenarioContainer
    {
        public AutofacAutoMockingContainer(IMockFactory mockFactory)
            : base(CreateBuilder(mockFactory))
        {
        }

        static ContainerBuilder CreateBuilder(IMockFactory mockFactory)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            containerBuilder.RegisterSource(new AutofacMockRegistrationHandler(mockFactory));
            return containerBuilder;
        }
    }
}
