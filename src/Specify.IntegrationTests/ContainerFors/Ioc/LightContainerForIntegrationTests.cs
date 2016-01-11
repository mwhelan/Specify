using Specify.Configuration;
using Specify.LightInject;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.ContainerFors.Ioc
{
    public class LightContainerForIntegrationTests : ContainerForIntegrationTestsBase
    {
        protected override ContainerFor<T> CreateSut<T>()
        {
            var serviceContainer = new LightContainerFactory().Create(null);
            var container = serviceContainer.GetInstance<IContainer>();
            container.Set<IDependency1, Dependency1>();
            container.Set<IDependency2, Dependency2>();
            container.Set<IDependency3, Dependency3>();
            container.Set<IDependency3, Dependency4>();
            container.Set<ConcreteObjectWithNoConstructor>();
            container.Set<ConcreteObjectWithMultipleConstructors>();
            container.Set<ConcreteObjectWithOneInterfaceCollectionConstructor>();
            return new ContainerFor<T>(container);
        }
    }
}
