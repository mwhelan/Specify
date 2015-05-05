using Ninject;
using Ninject.Extensions.NamedScope;
using Specify.Ninject;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.SutFactories
{
    public class SutFactoryNinjectContainerIntegrationTests : SutFactoryIntegrationTests
    {
        protected override SutFactory<T> CreateSut<T>()
        {
            var kernel = new StandardKernel();
            kernel
                .Bind(typeof(ConcreteObjectWithMultipleConstructors))
                .ToConstant(new ConcreteObjectWithMultipleConstructors(new Dependency1(),new Dependency2()))
                .DefinesNamedScope(NinjectDependencyResolver.ScenarioLifetimeScopeTag);
            kernel
                .Bind<ConcreteObjectWithNoConstructor>()
                .ToSelf()
                .DefinesNamedScope(NinjectDependencyResolver.ScenarioLifetimeScopeTag);
            var container = new NinjectContainer(kernel);
            container.Register<IDependency1, Dependency1>();
            container.Register<IDependency2, Dependency2>();
            container.Container.CreateNamedScope(NinjectDependencyResolver.ScenarioLifetimeScopeTag);
            return new SutFactory<T>(container);
        }
    }
}
