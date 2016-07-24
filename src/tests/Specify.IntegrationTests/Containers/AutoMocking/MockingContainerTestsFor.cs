using NUnit.Framework;
using Shouldly;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.Containers.AutoMocking
{
    public abstract class ContainerSpecsFor<T> where T : IContainer
    {
        protected abstract T CreateSut();
    }

    public abstract class MockingContainerTestsFor<T> : ContainerSpecsFor<T> 
        where T : IContainer
    {
        [Test]
        public void CanResolve_should_return_true_if_service_not_registered()
        {
            var sut = CreateSut();
            sut.CanResolve<ConcreteObjectWithOneInterfaceConstructor>().ShouldBe(true);
        }

        [Test]
        public void Set_should_register_service_by_type()
        {
            var sut = CreateSut();
            sut.Set<IDependency1, Dependency1>();
            sut.CanResolve<IDependency1>().ShouldBe(true);
        }

        [Test]
        public void Set_should_register_singleton_lifetime()
        {
            var sut = CreateSut();
            sut.Set<IDependency2, Dependency2>();
            sut.Get<IDependency2>().ShouldBeSameAs(sut.Get<IDependency2>());
        }
    }
}