using NUnit.Framework;
using Shouldly;
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
    }
}