using DryIoc;
using Specify.Containers;
using Specify.Mocks;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.ContainerFors.Ioc
{
    public class DryContainerForIntegrationTests : ContainerForIntegrationTestsBase
    {
        protected override ContainerFor<T> CreateSut<T>()
        {
            var dryContainer = new DryContainerFactory().Create(new NullMockFactory());
            var container = new DryContainer(dryContainer);
            container.Set<IDependency1, Dependency1>();
            container.Set<IDependency2, Dependency2>();
            container.SetMultiple<IDependency3>(new []{typeof(Dependency3), typeof(Dependency4)});
            container.Set<ConcreteObjectWithNoConstructor>();
            container.Set<ConcreteObjectWithMultipleConstructors>();
            container.Set<ConcreteObjectWithOneInterfaceCollectionConstructor>();
            return new ContainerFor<T>(container);
        }
    }
}
