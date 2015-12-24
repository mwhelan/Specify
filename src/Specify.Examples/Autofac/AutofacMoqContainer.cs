using Autofac;
using Autofac.Features.ResolveAnything;
using Specify.Autofac;
using Specify.Mocks;

namespace Specify.Examples.Autofac
{
    /// <summary>
    /// Automocking container that uses Moq to create mocks and Autofac as the container. 
    /// </summary>
    public class AutofacMoqContainer : AutofacContainer
    {
        public AutofacMoqContainer()
            : base(CreateBuilder())
        {
        }

        static ContainerBuilder CreateBuilder()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            containerBuilder.RegisterSource(new AutofacMockRegistrationHandler(new MoqMockFactory()));
            return containerBuilder;
        }
    }
}