using NUnit.Framework;
using Shouldly;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.Containers.Ioc
{
    [TestFixture]
    public abstract class IocContainerTestsFor<T> where T : IContainer
    {
        protected abstract T CreateSut();
        [Test]
        public void cannot_resolve_service_implementations_that_are_not_registered()
        {
            var sut = CreateSut();
            sut.CanResolve<ConcreteObjectWithOneInterfaceConstructor>().ShouldBe(false);
        }

        [Test]
        public void can_register_service_implementation()
        {
            var sut = CreateSut();
            sut.Register<IDependency1, Dependency1>();
            sut.CanResolve<IDependency1>().ShouldBe(true);
        }

        [Test]
        public void RegisterService_should_register_singleton_lifetime()
        {
            var sut = CreateSut();
            sut.Register<IDependency2, Dependency2>();
            sut.Resolve<IDependency2>().ShouldBeSameAs(sut.Resolve<IDependency2>());
        }
    }
}
