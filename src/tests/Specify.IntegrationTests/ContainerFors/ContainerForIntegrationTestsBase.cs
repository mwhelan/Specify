using NUnit.Framework;
using Shouldly;
using Specify.Exceptions;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.ContainerFors
{
    [TestFixture]
    public abstract class ContainerForIntegrationTestsBase
    {
        protected abstract ContainerFor<T> CreateSut<T>() where T : class;

        [Test]
        public void should_use_container_to_create_sut()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            var result = sut.SystemUnderTest;
            result.ShouldNotBe(null);
        }

        [Test]
        public void sut_should_be_singleton()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            var result = sut.SystemUnderTest;

            sut.SystemUnderTest.ShouldBeSameAs(result);
        }

        [Test]
        public void should_be_able_to_set_sut_independently()
        {
            var instance = new ConcreteObjectWithNoConstructor();
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            var original = sut.SystemUnderTest;

            sut.SystemUnderTest = instance;

            sut.SystemUnderTest.ShouldBeSameAs(instance);
            sut.SystemUnderTest.ShouldNotBeSameAs(original);
        }

        [Test]
        public void should_provide_sut_dependencies()
        {
            var sut = this.CreateSut<ConcreteObjectWithMultipleConstructors>();

            var result = sut.SystemUnderTest;

            result.Dependency1.ShouldNotBe(null);
            result.Dependency2.ShouldNotBe(null);
        }

        [Test]
        public void SetType_should_register_type_if_SUT_not_set()
        {
            var sut = this.CreateSut<ConcreteObjectWithMultipleConstructors>();
            sut.Set<Dependency1>();
            sut.Get<Dependency1>().ShouldNotBe(null);
        }

        [Test]
        public void SetType_should_throw_if_SUT_is_set()
        {
            var sut = this.CreateSut<ConcreteObjectWithMultipleConstructors>();
            var result = sut.SystemUnderTest;
            Should.Throw<InterfaceRegistrationException>(() => sut.Set<ConcreteObjectWithNoConstructor>())
                .Message.ShouldBe("Cannot register service Specify.Tests.Stubs.ConcreteObjectWithNoConstructor after SUT is created");
        }

        [Test]
        public void SetService_should_register_service_if_SUT_not_set()
        {
            var sut = this.CreateSut<ConcreteObjectWithMultipleConstructors>();
            sut.Set<IDependency2, Dependency2>();
            sut.Get<IDependency2>().ShouldNotBe(null);
        }

        [Test]
        public void SetService_should_throw_if_SUT_is_set()
        {
            var sut = this.CreateSut<ConcreteObjectWithMultipleConstructors>();
            var result = sut.SystemUnderTest;
            Should.Throw<InterfaceRegistrationException>(() => sut.Set<IDependency1,Dependency1>())
                .Message.ShouldBe("Cannot register service Specify.Tests.Stubs.Dependency1 after SUT is created");
        }

        [Test]
        public void SetInstance_should_register_instance_if_SUT_not_set()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            var instance = new Dependency3();

            sut.Set<IDependency3>(instance);

            sut.Get<IDependency3>().ShouldNotBe(null);
            sut.Get<IDependency3>().ShouldBeSameAs(instance);
        }

        [Test]
        public void SetInstance_should_throw_if_SUT_is_set()
        {
            var sut = this.CreateSut<ConcreteObjectWithMultipleConstructors>();
            var result = sut.SystemUnderTest;
            Should.Throw<InterfaceRegistrationException>(() => sut.Set(new ConcreteObjectWithNoConstructor()))
                .Message.ShouldBe("Cannot register service Specify.Tests.Stubs.ConcreteObjectWithNoConstructor after SUT is created");
        }

        [Test]
        public void SetInstance_named_should_register_separate_named_instances()
        {
            var sut = this.CreateSut<ConcreteObjectWithMultipleConstructors>();
            var instance1 = new Dependency3();
            var instance2 = new Dependency3();

            sut.Set<Dependency3>(instance1, "instance1");
            sut.Set<Dependency3>(instance2, "instance2");

            sut.Get<Dependency3>("instance1").ShouldBeSameAs(instance1);
            sut.Get<Dependency3>("instance2").ShouldBeSameAs(instance2);
        }

        [Test]
        public void SetInstance_unnamed_should_return_unnamed_when_multiple_registrations()
        {
            var sut = this.CreateSut<ConcreteObjectWithMultipleConstructors>();
            var instance1 = new Dependency3();
            var instance2 = new Dependency3();

            sut.Set<Dependency3>(instance1, "instance1");
            sut.Set<Dependency3>(instance2);

            sut.Get<Dependency3>("instance1").ShouldBeSameAs(instance1);
            sut.Get<Dependency3>().ShouldBeSameAs(instance2);
        }

        [Test]
        public void Get_generic_should_resolve_service()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            var result = sut.Get<ConcreteObjectWithNoConstructor>();
            result.ShouldNotBe(null);
        }

        [Test]
        public void Get_should_resolve_service()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            var result = sut.Get(typeof(ConcreteObjectWithNoConstructor));
            sut.ShouldNotBe(null);
        }

        [Test]
        public void CanResolve_generic_should_return_true_if_service_is_registered()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            var result = sut.CanResolve<ConcreteObjectWithNoConstructor>();
            result.ShouldBe(true);
        }

        [Test]
        public void CanResolve_should_return_true_if_service_is_registered()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            var result = sut.CanResolve(typeof(ConcreteObjectWithNoConstructor));
            result.ShouldBe(true);
        }

        [Test]
        public void SetType_should_register_singleton_lifetime()
        {
            var sut = this.CreateSut<ConcreteObjectWithMultipleConstructors>();
            sut.Set<Dependency1>();
            sut.Get<Dependency1>().ShouldBeSameAs(sut.Get<Dependency1>());
        }

        [Test]
        public void SetService_should_register_singleton_lifetime()
        {
            var sut = this.CreateSut<ConcreteObjectWithMultipleConstructors>();
            sut.Set<IDependency2, Dependency2>();
            sut.Get<IDependency2>().ShouldBeSameAs(sut.Get<IDependency2>());
        }

        [Test]
        public void SetInstance_should_register_singleton_lifetime()
        {
            var sut = this.CreateSut<ConcreteObjectWithMultipleConstructors>();
            sut.Set<Dependency1>(new Dependency1());
            sut.Get<Dependency1>().ShouldBeSameAs(sut.Get<Dependency1>());
        }

        /// <summary>
        /// Temporarily here while I build out this feature for all containers.
        /// Will live in ContainerForIntegrationTestsBase once all Containers have this feature.
        /// </summary>
        //[Test]
        //public virtual void SystemUnderTest_should_resolve_collection_in_constructor()
        //{
        //    var sut = this.CreateSut<ConcreteObjectWithOneInterfaceCollectionConstructor>();
        //    var result = sut.SystemUnderTest;
        //    result.Collection.ToList().Count.ShouldBe(2);
        //}
    }
}