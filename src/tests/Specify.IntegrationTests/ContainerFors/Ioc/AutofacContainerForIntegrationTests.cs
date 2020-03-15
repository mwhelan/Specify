using Autofac;
using Specify.Autofac;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.ContainerFors.Ioc
{
    public class AutofacContainerForIntegrationTests : ContainerForIntegrationTestsBase
    {
        protected override ContainerFor<T> CreateSut<T>()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Dependency4>().As<IDependency3>();
            builder.RegisterType<Dependency3>().As<IDependency3>();

            builder.RegisterType<Dependency1>().As<IDependency1>();
            builder.RegisterType<Dependency2>().As<IDependency2>();
            builder.RegisterType<ConcreteObjectWithNoConstructor>();
            builder.RegisterType<ConcreteObjectWithMultipleConstructors>();
            builder.RegisterType<ConcreteObjectWithOneInterfaceCollectionConstructor>();

            var container = new AutofacContainer(builder.Build());
            return new ContainerFor<T>(container);
        }
    }
}