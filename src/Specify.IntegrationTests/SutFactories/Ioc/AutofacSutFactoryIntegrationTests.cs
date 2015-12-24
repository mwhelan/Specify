using Specify.Autofac;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.SutFactories.Ioc
{
    public class AutofacSutFactoryIntegrationTests : SutFactoryIntegrationTestsBase
    {
        protected override SutFactory<T> CreateSut<T>()
        {
            var container = new AutofacContainer();
            container.Register<IDependency1, Dependency1>();
            container.Register<IDependency2, Dependency2>();
            container.Register<ConcreteObjectWithMultipleConstructors>();
            container.Register<ConcreteObjectWithNoConstructor>();
            return new SutFactory<T>(container);
        }
    }
}