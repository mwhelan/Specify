using Specify.Tests.Stubs;
using TinyIoC;

namespace Specify.IntegrationTests.ContainerFors.Ioc
{
    public class TinyContainerForIntegrationTests : ContainerForIntegrationTestsBase
    {
        protected override ContainerFor<T> CreateSut<T>()
        {
            var container = new TinyContainer(new TinyIoCContainer());
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
