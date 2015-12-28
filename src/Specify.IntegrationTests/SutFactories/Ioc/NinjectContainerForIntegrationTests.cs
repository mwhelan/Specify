using Ninject;
using Specify.Ninject;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.SutFactories.Ioc
{
    public class NinjectContainerForIntegrationTests : ContainerForIntegrationTestsBase
    {
        protected override ContainerFor<T> CreateSut<T>()
        {
            var kernel = new StandardKernel();
            var container = new NinjectContainer(kernel);
            container.Register<IDependency1, Dependency1>();
            container.Register<IDependency2, Dependency2>();
            container.Register(new ConcreteObjectWithMultipleConstructors(new Dependency1(), new Dependency2()));
            container.Register<ConcreteObjectWithNoConstructor>();
            return new ContainerFor<T>(container);
        }
    }
}
