using System;
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
        public void CanResolve_should_return_true_if_service_not_registered_and_dependency_chains_are_resolvable()
        {
            var sut = CreateSut();
            sut.CanResolve<ConcreteObjectWithOneInterfaceConstructor>().ShouldBe(true);
            sut.CanResolve<ConcreteObjectWithOneInterfaceCollectionConstructor>().ShouldBe(true);
            sut.CanResolve<ConcreteObjectWithOneConcreteConstructor>().ShouldBe(true);
            sut.CanResolve<ConcreteObjectWithMultipleConstructors>().ShouldBe(true);
            sut.CanResolve<ConcreteObjectWithOneSealedConstructorHavingNoConstructor>().ShouldBe(true);
            sut.CanResolve<ConcreteObjectWithOneSealedConstructorHavingOneInterfaceConstructor>().ShouldBe(true);
        }

        [Test]
        public void CanResolve_should_return_false_if_service_not_registered_and_dependency_chains_are_not_resolvable()
        {
            var sut = CreateSut();
            sut.CanResolve<ConcreteObjectWithPrivateConstructor>().ShouldBe(false);
            sut.CanResolve<ConcreteObjectWithOneConcreteConstructorHavingPrivateConstructor>().ShouldBe(false);
            sut.CanResolve<ConcreteObjectWithOneSealedConstructorHavingPrivateConstructor>().ShouldBe(false);
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

        [Test]
        public void Get_should_resolve_for_all_concrete_types_where_dependency_chains_are_resolvable()
        {
            var sut = CreateSut();

            sut.Get<SealedDependencyWithNoConstructor>().ShouldNotBeNull();
            sut.Get<SealedDependencyWithOneInterfaceConstructor>().ShouldNotBeNull();

            sut.Get<ConcreteObjectWithOneInterfaceConstructor>().ShouldNotBeNull();
            sut.Get<ConcreteObjectWithOneInterfaceCollectionConstructor>().ShouldNotBeNull();
            sut.Get<ConcreteObjectWithOneConcreteConstructor>().ShouldNotBeNull();
            sut.Get<ConcreteObjectWithMultipleConstructors>().ShouldNotBeNull();

            var resolvedConcreteObjectWithOneSealedConstructorHavingNoConstructor = sut.Get<ConcreteObjectWithOneSealedConstructorHavingNoConstructor>();
            resolvedConcreteObjectWithOneSealedConstructorHavingNoConstructor.ShouldNotBeNull();
            resolvedConcreteObjectWithOneSealedConstructorHavingNoConstructor.SealedDependencyWithNoConstructor.ShouldNotBeNull();

            var resolvedConcreteObjectWithOneSealedConstructorHavingOneInterfaceConstructor = sut.Get<ConcreteObjectWithOneSealedConstructorHavingOneInterfaceConstructor>();
            resolvedConcreteObjectWithOneSealedConstructorHavingOneInterfaceConstructor.ShouldNotBeNull();
            resolvedConcreteObjectWithOneSealedConstructorHavingOneInterfaceConstructor.SealedDependencyWithOneInterfaceConstructor.ShouldNotBeNull();
        }

        [Test]
        public void Get_should_not_resolve_for_any_concrete_types_where_dependency_chains_are_not_resolvable()
        {
            var sut = CreateSut();

            try
            {
                sut.Get<ConcreteObjectWithPrivateConstructor>();
                Assert.Fail();
            }
            catch (Exception e)
            {
                e.ShouldNotBeNull();
            }
            try
            {
                sut.Get<ConcreteObjectWithOneConcreteConstructorHavingPrivateConstructor>();
                Assert.Fail();
            }
            catch (Exception e)
            {
                e.ShouldNotBeNull();
            }
            try
            {
                sut.Get<ConcreteObjectWithOneSealedConstructorHavingPrivateConstructor>();
                Assert.Fail();
            }
            catch (Exception e)
            {
                e.ShouldNotBeNull();
            }
        }
    }
}