using System;
using NUnit.Framework;
using Shouldly;
using Specify.Containers;
using Specify.Tests.Stubs;

namespace Specify.Tests.Containers.SutFactory
{
    [TestFixture]
    public abstract class SutFactoryIntegrationTests
    {
        protected abstract SutFactory<T> CreateSut<T>() where T : class;

        [Test]
        public void should_use_container_to_create_sut()
        {
            var sut = CreateSut<ConcreteObjectWithNoConstructor>();
            var result = sut.SystemUnderTest;
            result.ShouldNotBe(null);
        }

        [Test]
        public void sut_should_be_singleton()
        {
            var sut = CreateSut<ConcreteObjectWithNoConstructor>();
            var result = sut.SystemUnderTest;

            sut.SystemUnderTest.ShouldBeSameAs(result);
        }

        [Test]
        public void should_be_able_to_set_sut_independently()
        {
            var instance = new ConcreteObjectWithNoConstructor();
            var sut = CreateSut<ConcreteObjectWithNoConstructor>();
            var original = sut.SystemUnderTest;

            sut.SystemUnderTest = instance;

            sut.SystemUnderTest.ShouldBeSameAs(instance);
            sut.SystemUnderTest.ShouldNotBeSameAs(original);
        }

        [Test]
        public void should_provide_sut_dependencies()
        {
            var sut = CreateSut<ConcreteObjectWithMultipleConstructors>();

            var result = sut.SystemUnderTest;

            result.Dependency1.ShouldNotBe(null);
            result.Dependency2.ShouldNotBe(null);
        }


        [Test]
        public void RegisterType_should_throw_if_SUT_is_set()
        {
            var sut = CreateSut<ConcreteObjectWithMultipleConstructors>();
            var result = sut.SystemUnderTest;
            Should.Throw<InvalidOperationException>(() => sut.Register<ConcreteObjectWithNoConstructor>())
                .Message.ShouldBe("Cannot register type after SUT is created.");
        }

        [Test]
        public void RegisterService_should_register_service_if_SUT_not_set()
        {
            var sut = CreateSut<ConcreteObjectWithMultipleConstructors>();
            sut.Register<IDependency2, Dependency2>();
            sut.Get<IDependency2>().ShouldNotBe(null);
        }

        [Test]
        public void RegisterService_should_throw_if_SUT_is_set()
        {
            var sut = CreateSut<ConcreteObjectWithMultipleConstructors>();
            var result = sut.SystemUnderTest;
            Should.Throw<InvalidOperationException>(() => sut.Register<IDependency1,Dependency1>())
                .Message.ShouldBe("Cannot register service after SUT is created.");
        }

        [Test]
        public void RegisterType_should_register_type_if_SUT_not_set()
        {
            var sut = CreateSut<ConcreteObjectWithMultipleConstructors>();
            sut.Register<Dependency1>();
            sut.Get<Dependency1>().ShouldNotBe(null);
        }

        [Test]
        public void RegisterInstance_should_throw_if_SUT_is_set()
        {
            var sut = CreateSut<ConcreteObjectWithMultipleConstructors>();
            var result = sut.SystemUnderTest;
            Should.Throw<InvalidOperationException>(() => sut.Register(new ConcreteObjectWithNoConstructor()))
                .Message.ShouldBe("Cannot register instance after SUT is created.");
        }

        [Test]
        public void RegisterInstance_should_register_instance_if_SUT_not_set()
        {
            var sut = CreateSut<ConcreteObjectWithNoConstructor>();
            var instance = new Dependency3();

            sut.Register<IDependency3>(instance);

            sut.Get<IDependency3>().ShouldNotBe(null);
            sut.Get<IDependency3>().ShouldBeSameAs(instance);
        }

        [Test]
        public void RegisterInstance_named_should_register_separate_named_instances()
        {
            var sut = CreateSut<ConcreteObjectWithMultipleConstructors>();
            var instance1 = new Dependency3();
            var instance2 = new Dependency3();

            sut.Register<Dependency3>(instance1, "instance1");
            sut.Register<Dependency3>(instance2, "instance2");

            sut.Get<Dependency3>("instance1").ShouldBeSameAs(instance1);
            sut.Get<Dependency3>("instance2").ShouldBeSameAs(instance2);
        }

        [Test]
        public void RegisterInstance_unnamed_should_return_unnamed_when_multiple_registrations()
        {
            var sut = CreateSut<ConcreteObjectWithMultipleConstructors>();
            var instance1 = new Dependency3();
            var instance2 = new Dependency3();

            sut.Register<Dependency3>(instance1, "instance1");
            sut.Register<Dependency3>(instance2);

            sut.Get<Dependency3>("instance1").ShouldBeSameAs(instance1);
            sut.Get<Dependency3>().ShouldBeSameAs(instance2);
        }

        [Test]
        public void RegisterType_should_register_transient_lifetime()
        {
            var sut = CreateSut<ConcreteObjectWithMultipleConstructors>();
            sut.Register<Dependency1>();
            sut.Get<Dependency1>().ShouldNotBeSameAs(sut.Get<Dependency1>());
        }

        [Test]
        public void RegisterInstance_should_register_singleton_lifetime()
        {
            var sut = CreateSut<ConcreteObjectWithMultipleConstructors>();
            sut.Register<Dependency1>(new Dependency1());
            sut.Get<Dependency1>().ShouldBeSameAs(sut.Get<Dependency1>());
        }

        [Test]
        public void RegisterService_should_register_singleton_lifetime()
        {
            var sut = CreateSut<ConcreteObjectWithMultipleConstructors>();
            sut.Register<IDependency2, Dependency2>();
            sut.Get<IDependency2>().ShouldBeSameAs(sut.Get<IDependency2>());
        }

        [Test]
        public void Resolve_generic_should_call_container_resolve_generic()
        {
            var sut = CreateSut<ConcreteObjectWithNoConstructor>();
            var result = sut.Get<ConcreteObjectWithNoConstructor>();
            result.ShouldNotBe(null);
        }

        [Test]
        public void Resolve_should_call_container_resolve()
        {
            var sut = CreateSut<ConcreteObjectWithNoConstructor>();
            var result = sut.Get(typeof(ConcreteObjectWithNoConstructor));
            sut.ShouldNotBe(null);
        }

        [Test]
        public void IsRegistered_generic_should_call_container_IsRegistered_generic()
        {
            var sut = CreateSut<ConcreteObjectWithNoConstructor>();
            var result = sut.IsRegistered<ConcreteObjectWithNoConstructor>();
            result.ShouldBe(true);
        }

        [Test]
        public void IsRegistered_should_call_container_IsRegistered()
        {
            var sut = CreateSut<ConcreteObjectWithNoConstructor>();
            var result = sut.IsRegistered(typeof(ConcreteObjectWithNoConstructor));
            result.ShouldBe(true);
        }
    }
}