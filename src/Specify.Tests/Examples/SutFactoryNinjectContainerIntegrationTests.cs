using Ninject;
using NUnit.Framework;
using Shouldly;
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
            var container = new NinjectContainer();
            container.Register<IDependency1, Dependency1>();
            container.Register<IDependency2, Dependency2>();
            container.Register<ConcreteObjectWithMultipleConstructors>();
            container.Register<ConcreteObjectWithNoConstructor>();
            return new SutFactory<T>(container);
        }
    }

    //[TestFixture]
    //public class NinjectSandbox
    //{
    //    [Test]
    //    public void RegisterInstance_unnamed_should_return_unnamed_when_multiple_registrations()
    //    {
    //        var sut = new StandardKernel();
    //        var instance1 = new Dependency3();
    //        var instance2 = new Dependency3();

    //        sut.Bind<Dependency3>().ToConstant(instance1).Named("instance1");
    //        sut.Bind<Dependency3>().ToConstant(instance2);

    //        sut.Get<Dependency3>("instance1").ShouldBeSameAs(instance1);
    //        sut.Get<Dependency3>("").ShouldBeSameAs(instance2);
    //    }
    //}
}
