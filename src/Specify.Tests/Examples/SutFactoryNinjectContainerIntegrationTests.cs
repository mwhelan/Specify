using Ninject;
using Specify.Containers;
using Specify.Examples.Ninject;
using Specify.Tests.Containers.SutFactory;
using Specify.Tests.Stubs;

namespace Specify.Tests.Examples
{
    public class SutFactoryNinjectContainerIntegrationTests : SutFactoryIntegrationTests
    {
        protected override SutFactory<T> CreateSut<T>()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IDependency1>().To<Dependency1>();
            kernel.Bind<IDependency2>().To<Dependency2>();
            kernel.Bind<ConcreteObjectWithMultipleConstructors>().ToSelf();
            kernel.Bind<ConcreteObjectWithNoConstructor>().ToSelf();
            var container = new NinjectContainer(kernel);
            return new SutFactory<T>(container);
        }
    }
}
