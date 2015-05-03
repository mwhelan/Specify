using Autofac;
using Autofac.Features.ResolveAnything;
using Specify.Autofac;
using Specify.Examples.Mocks;

namespace Specify.Examples.Autofac
{
    /// <summary>
    /// Automocking container that uses FakeItEasy to create mocks and Autofac as the container. 
    /// </summary>
    public class AutofacFakeItEasyContainer : IocContainer
    {
        public AutofacFakeItEasyContainer()
            : base(CreateBuilder())
        {
        }

        static ContainerBuilder CreateBuilder()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            containerBuilder.RegisterSource(new AutofacMockRegistrationHandler(new FakeItEasyMockFactory()));
            return containerBuilder;
        }
    }
}