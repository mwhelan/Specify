using NUnit.Framework;
using Shouldly;
using Specify.IntegrationTests.Containers.AutoMocking;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.Containers.Ioc
{
    [TestFixture]
    public abstract class IocContainerTestsFor<T> : ContainerSpecsFor<T> 
        where T : IContainer
    {
        [Test]
        public void CanResolve_should_return_false_if_service_not_registered()
        {
            var sut = CreateSut();

            sut.CanResolve<ConcreteObjectWithNoConstructor>().ShouldBe(false);
            sut.CanResolve<IDependency1>().ShouldBe(false);
        }

        [Test]
        public void CanResolve_should_return_true_if_service_is_registered()
        {
            var sut = CreateSut();
            sut.Set<ConcreteObjectWithNoConstructor>();
            sut.CanResolve<ConcreteObjectWithNoConstructor>().ShouldBe(true);
        }

        [Test]
        public void CanResolve_should_return_false_if_class_is_registered_but_dependency_is_not()
        {
            var sut = CreateSut();
            sut.Set<ConcreteObjectWithOneInterfaceConstructor>();
            sut.CanResolve<ConcreteObjectWithOneInterfaceConstructor>().ShouldBe(false);
        }

        [Test]
        public void CanResolve_should_return_true_if_class_and_all_dependencies_registered()
        {
            var sut = CreateSut();

            sut.Set<ConcreteObjectWithOneInterfaceConstructor>();
            sut.Set<IDependency1, Dependency1>();

            sut.CanResolve<ConcreteObjectWithOneInterfaceConstructor>().ShouldBe(true);
        }

        [Test]
        public void can_register_service_implementation()
        {
            var sut = CreateSut();
            sut.Set<IDependency1, Dependency1>();
            sut.CanResolve<IDependency1>().ShouldBe(true);
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
