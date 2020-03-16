using System;
using NUnit.Framework;
using Shouldly;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.Containers.AutoMocking
{
    public abstract class MockingContainerTestsFor<T> : ContainerSpecsFor<T> 
        where T : IContainerRoot
    {
        public T SUT { get; set; }

        [SetUp]
        public void SetUp()
        {
            SUT = CreateSut();
        }

        [Test]
        public void CanResolve_should_return_true_if_service_not_registered_and_dependency_chains_are_resolvable()
        {
            SUT.CanResolve<ConcreteObjectWithOneInterfaceConstructor>().ShouldBe(true);
            SUT.CanResolve<ConcreteObjectWithOneInterfaceCollectionConstructor>().ShouldBe(true);
            SUT.CanResolve<ConcreteObjectWithOneConcreteConstructor>().ShouldBe(true);
            SUT.CanResolve<ConcreteObjectWithMultipleConstructors>().ShouldBe(true);
            SUT.CanResolve<ConcreteObjectWithOneSealedConstructorHavingNoConstructor>().ShouldBe(true);
            SUT.CanResolve<ConcreteObjectWithOneSealedConstructorHavingOneInterfaceConstructor>().ShouldBe(true);
        }

        [Test]
        public void CanResolve_should_return_false_if_service_not_registered_and_dependency_chains_are_not_resolvable()
        {
            SUT.CanResolve<ConcreteObjectWithPrivateConstructor>().ShouldBe(false);
            SUT.CanResolve<ConcreteObjectWithOneConcreteConstructorHavingPrivateConstructor>().ShouldBe(false);
            SUT.CanResolve<ConcreteObjectWithOneSealedConstructorHavingPrivateConstructor>().ShouldBe(false);
        }

        [Test]
        public void Set_should_register_service_by_type()
        {
            SUT.Set<IDependency1, Dependency1>();
            SUT.CanResolve<IDependency1>().ShouldBe(true);
        }

        [Test]
        public void Set_should_register_singleton_lifetime()
        {
            SUT.Set<IDependency2, Dependency2>();
            SUT.Get<IDependency2>().ShouldBeSameAs(SUT.Get<IDependency2>());
        }

        [Test]
        public void Get_should_resolve_for_all_concrete_types_where_dependency_chains_are_resolvable()
        {
            SUT.Get<SealedDependencyWithNoConstructor>().ShouldNotBeNull();
            SUT.Get<SealedDependencyWithOneInterfaceConstructor>().ShouldNotBeNull();

            SUT.Get<ConcreteObjectWithOneInterfaceConstructor>().ShouldNotBeNull();
            SUT.Get<ConcreteObjectWithOneInterfaceCollectionConstructor>().ShouldNotBeNull();
            SUT.Get<ConcreteObjectWithOneConcreteConstructor>().ShouldNotBeNull();
            SUT.Get<ConcreteObjectWithMultipleConstructors>().ShouldNotBeNull();

            var resolvedConcreteObjectWithOneSealedConstructorHavingNoConstructor = SUT.Get<ConcreteObjectWithOneSealedConstructorHavingNoConstructor>();
            resolvedConcreteObjectWithOneSealedConstructorHavingNoConstructor.ShouldNotBeNull();
            resolvedConcreteObjectWithOneSealedConstructorHavingNoConstructor.SealedDependencyWithNoConstructor.ShouldNotBeNull();

            var resolvedConcreteObjectWithOneSealedConstructorHavingOneInterfaceConstructor = SUT.Get<ConcreteObjectWithOneSealedConstructorHavingOneInterfaceConstructor>();
            resolvedConcreteObjectWithOneSealedConstructorHavingOneInterfaceConstructor.ShouldNotBeNull();
            resolvedConcreteObjectWithOneSealedConstructorHavingOneInterfaceConstructor.SealedDependencyWithOneInterfaceConstructor.ShouldNotBeNull();
        }

        [Test]
        public void Get_should_not_resolve_for_any_concrete_types_where_dependency_chains_are_not_resolvable()
        {
            //ShouldNotResolve<ConcreteObjectWithPrivateConstructor>();
            ShouldNotResolve<ConcreteObjectWithOneConcreteConstructorHavingPrivateConstructor>();
           // ShouldNotResolve<ConcreteObjectWithOneSealedConstructorHavingPrivateConstructor>();
        }

        private void ShouldNotResolve<TService>() where TService : class
        {
            try
            {
                var instance = SUT.Get<TService>();
                Assert.Fail($"{SUT.GetType().Name} should not have resolved {typeof(TService).Name}");
            }
            catch (Exception e)
            {
                e.ShouldNotBeNull();
            }
        }
    }
}