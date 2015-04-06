using Autofac;
using Autofac.Features.ResolveAnything;
using Specify.Containers;
using Specify.Containers.Mocking;

namespace Specify.Examples.Autofac
{
    /// <summary>
    /// Automocking container that uses NSubstitute to create mocks and Autofac as the container. 
    /// </summary>
    public class AutofacNSubstituteContainer : IocContainer
    {
        public AutofacNSubstituteContainer()
            : base(CreateBuilder())
        {
        }

        static ContainerBuilder CreateBuilder()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            containerBuilder.RegisterSource(new AutofacMockRegistrationHandler(new NSubstituteMockFactory()));
            return containerBuilder;
        }
    }
}