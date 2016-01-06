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

            var container = new AutofacContainer(builder.Build());
            container.Register<IDependency1, Dependency1>();
            container.Register<IDependency2, Dependency2>();
            //container.Register<IDependency3, Dependency3>();
            container.Register<ConcreteObjectWithNoConstructor>();
            container.Register<ConcreteObjectWithMultipleConstructors>();
            container.Register<ConcreteObjectWithOneInterfaceCollectionConstructor>();
            return new ContainerFor<T>(container);
        }
    }
}