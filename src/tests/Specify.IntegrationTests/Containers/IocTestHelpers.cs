using Autofac;
using Specify.Autofac;
using Specify.Configuration.Examples;
using Specify.Containers;
using Specify.Mocks;
using TinyIoC;

namespace Specify.IntegrationTests.Containers
{
    public static class IocTestHelpers
    {
        public static ContainerBuilder InitializeAutofaContainerBuilder()
        {
            var builder = new ContainerBuilder();

            builder.RegisterTypes();
            builder.Register<IContainer>(c => new AutofacContainer(c.Resolve<ILifetimeScope>().BeginLifetimeScope()));

            return builder;
        }

        public static TinyIoCContainer InitializeTinyIoCContainer(IMockFactory mockFactory = null)
        {
            var container = new TinyIoCContainer();

            if (mockFactory == null)
            {
                container.Register<IContainerRoot>((c, p) => new TinyContainer(c));
            }
            else
            {
                container.Register<IContainerRoot>((c, p) => new TinyMockingContainer(mockFactory, c));
            }


            container.Register<IContainer>((c, p) => new TinyContainer(c.GetChildContainer()));
            container.Register<IExampleScope, ExampleScope>();
            container.Register<IChildContainerBuilder, TinyChildContainerBuilder>();

            return container;
        }
    }
}