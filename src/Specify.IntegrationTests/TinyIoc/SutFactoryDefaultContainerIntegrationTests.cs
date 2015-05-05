using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.TinyIoc
{
    public class SutFactoryDefaultContainerIntegrationTests : SutFactoryIntegrationTests
    {
        protected override SutFactory<T> CreateSut<T>()
        {
            var container = new DefaultContainer();
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
