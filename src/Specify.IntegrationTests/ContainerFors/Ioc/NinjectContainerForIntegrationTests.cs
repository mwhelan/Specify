using Ninject;
using Specify.Ninject;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.ContainerFors.Ioc
{
    public class NinjectContainerForIntegrationTests : ContainerForIntegrationTestsBase
    {
        protected override ContainerFor<T> CreateSut<T>()
        {
            var kernel = new StandardKernel();
            var container = new NinjectContainer(kernel);
            container.Set<IDependency1, Dependency1>();
            container.Set<IDependency2, Dependency2>();
            container.Set(new ConcreteObjectWithMultipleConstructors(new Dependency1(), new Dependency2()));
            container.Set<ConcreteObjectWithNoConstructor>();
            return new ContainerFor<T>(container);
        }
    }
}
