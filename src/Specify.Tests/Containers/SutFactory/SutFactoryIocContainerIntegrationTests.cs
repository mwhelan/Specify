using Specify.Containers;
using Specify.Tests.Stubs;

namespace Specify.Tests.Containers.SutFactory
{
    public class SutFactoryIocContainerIntegrationTests : SutFactoryIntegrationTests
    {
        protected override SutFactory<T> CreateSut<T>()
        {
            var container = new IocContainer();
            container.Register<IDependency1, Dependency1>();
            container.Register<IDependency2, Dependency2>();
            container.Register<ConcreteObjectWithMultipleConstructors>();
            container.Register<ConcreteObjectWithNoConstructor>();
            return new SutFactory<T>(container);
        }
    }
}