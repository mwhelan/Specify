using FluentAssertions;
using NUnit.Framework;
using Specify.Providers;
using Specify.Tests.Stubs;

namespace Specify.Tests.Providers
{
    [TestFixture]
    public class AutofacNSubstituteTestLifetimeScopeSpecs
    {
        [Test]
        public void should_create_sut_with_no_constructor()
        {
            var sut = new AutofacNSubstituteTestLifetimeScope();
            sut.SystemUnderTest<ConcreteObjectWithNoConstructor>()
                .Should().BeOfType<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void sut_should_be_a_singleton()
        {
            var sut = new AutofacNSubstituteTestLifetimeScope();
            var expected = sut.SystemUnderTest<ConcreteObjectWithNoConstructor>();
            var result = sut.SystemUnderTest<ConcreteObjectWithNoConstructor>();
            result.Should().BeSameAs(expected);
        }

        [Test]
        public void should_create_sut_with_greediest_constructor()
        {
            var sut = new AutofacNSubstituteTestLifetimeScope();
            var result = sut.SystemUnderTest<ConcreteObjectWithMultipleConstructors>();

            result.Should().BeOfType<ConcreteObjectWithMultipleConstructors>();
            result.Dependency1.Should().NotBeNull();
            result.Dependency2.Should().NotBeNull();
        }

        [Test]
        public void should_be_able_to_inject_dependencies_into_sut_before_sut_creation()
        {
            var sut = new AutofacNSubstituteTestLifetimeScope();
            sut.RegisterInstance<IDependency1>(new Dependency1 { Value = 15 });

            var result = sut.SystemUnderTest<ConcreteObjectWithMultipleConstructors>();

            result.Dependency1.Value.Should().Be(15);
        }

        //[Test]
        //public void should_not_be_able_to_inject_dependencies_after_sut_has_been_created()
        //{
        //    var sut = CreateSut<ConcreteObjectWithMultipleConstructors>();
        //    sut.SystemUnderTest();

        //    Action result = () => sut.InjectDependency<IDependency1>(new Dependency1());

        //    result.ShouldThrow<InvalidOperationException>()
        //        .WithMessage("Cannot inject dependencies after the System Under Test has been created");
        //}

        [Test]
        public void sut_dependency_should_be_a_singleton()
        {
            var sut = new AutofacNSubstituteTestLifetimeScope();
            var expected = sut.Resolve<IDependency2>();
            var result = sut.Resolve<IDependency2>();
            result.Should().BeSameAs(expected);
        }

        [Test]
        public void non_sut_dependency_should_be_a_singleton()
        {
            var sut = new AutofacNSubstituteTestLifetimeScope();
            var expected = sut.Resolve<IDependency3>();

            var result = sut.Resolve<IDependency3>();

            result.Should().BeSameAs(expected);
        }
    }
}
