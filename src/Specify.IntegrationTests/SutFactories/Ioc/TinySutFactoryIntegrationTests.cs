using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.SutFactories.Ioc
{
    public class TinySutFactoryIntegrationTests : SutFactoryIntegrationTestsBase
    {
        protected override SutFactory<T> CreateSut<T>()
        {
            var container = new TinyContainer();
            container.Register<IDependency1, Dependency1>();
            container.Register<IDependency2, Dependency2>();
            container.Register<ConcreteObjectWithNoConstructor>();
            container.Register<ConcreteObjectWithMultipleConstructors>();
            return new SutFactory<T>(container);
        }
    }
    //public class SutFactoryTinyNSubstituteContainerIntegrationTests : SutFactoryIntegrationTests
    //{
    //    protected override SutFactory<T> CreateSut<T>()
    //    {
    //        var container = new TinyAutoMockingContainer(new NSubstituteMockFactory());
    //        return new SutFactory<T>(container);
    //    }
    //}
}
