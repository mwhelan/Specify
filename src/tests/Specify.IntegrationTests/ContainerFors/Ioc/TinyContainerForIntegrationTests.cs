using Specify.Tests.Stubs;
using TinyIoC;

namespace Specify.IntegrationTests.ContainerFors.Ioc
{
    public class TinyContainerForIntegrationTests : ContainerForIntegrationTestsBase
    {
        protected override ContainerFor<T> CreateSut<T>()
        {
            var container = new TinyIoCContainer();

            container.Register<IDependency1, Dependency1>();
            container.Register<IDependency2, Dependency2>();
            container.RegisterMultiple<IDependency3>(new[] { typeof(Dependency3), typeof(Dependency4) });
            container.Register<ConcreteObjectWithNoConstructor>();
            container.Register<ConcreteObjectWithMultipleConstructors>();
            container.Register<ConcreteObjectWithOneInterfaceCollectionConstructor>();

            var tinyContainer = new TinyContainer(container);

            return new ContainerFor<T>(tinyContainer);
        }
    }
}
