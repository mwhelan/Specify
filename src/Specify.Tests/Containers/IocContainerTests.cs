using NUnit.Framework;
using Shouldly;
using Specify.Autofac;
using Specify.Tests.Stubs;

namespace Specify.Tests.Containers
{
    [TestFixture]
    public class IocContainerTests
    {
        [Test]
        public void cannot_resolve_concrete_types_not_registered()
        {
            var sut = new IocContainer();
            sut.CanResolve<ConcreteObjectWithOneConcreteConstructor>().ShouldBe(false);
            sut.CanResolve<Dependency1>().ShouldBe(false);
        }

        [Test]
        public void cannot_resolve_service_implementations_that_are_not_registered()
        {
            var sut = new IocContainer();
            sut.CanResolve<ConcreteObjectWithOneInterfaceConstructor>().ShouldBe(false);
        }

        [Test]
        public void can_register_service_implementation()
        {
            var sut = new IocContainer();
            sut.Register<IDependency1, Dependency1>();
            sut.CanResolve<IDependency1>().ShouldBe(true);
        }

        [Test]
        public void RegisterService_should_register_singleton_lifetime()
        {
            var sut = new IocContainer();
            sut.Register<IDependency2, Dependency2>();
            sut.Resolve<IDependency2>().ShouldBeSameAs(sut.Resolve<IDependency2>());
        }
    }


}
