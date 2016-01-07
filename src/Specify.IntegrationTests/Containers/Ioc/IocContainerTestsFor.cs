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
            sut.CanGet<ConcreteObjectWithOneInterfaceConstructor>().ShouldBe(false);
        }

        [Test]
        public void can_register_service_implementation()
        {
            var sut = CreateSut();
            sut.Set<IDependency1, Dependency1>();
            sut.CanGet<IDependency1>().ShouldBe(true);
        }

        [Test]
        public void RegisterService_should_register_singleton_lifetime()
        {
            var sut = CreateSut();
            sut.Set<IDependency2, Dependency2>();
            sut.Get<IDependency2>().ShouldBeSameAs(sut.Get<IDependency2>());
        }
    }
}
