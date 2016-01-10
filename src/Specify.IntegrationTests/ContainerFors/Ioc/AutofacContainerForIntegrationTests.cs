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
            container.Set<IDependency1, Dependency1>();
            container.Set<IDependency2, Dependency2>();
            //container.Register<IDependency3, Dependency3>();
            container.Set<ConcreteObjectWithNoConstructor>();
            container.Set<ConcreteObjectWithMultipleConstructors>();
            container.Set<ConcreteObjectWithOneInterfaceCollectionConstructor>();
            return new ContainerFor<T>(container);
        }
    }
}