using System;
using FluentAssertions;
using NUnit.Framework;
using Specify.Tests.Stubs;
using Specify.WithAutofacNSubstitute;
using TestStack.BDDfy;

namespace Specify.Tests.WithAutofacNSubstitute
{
    public class CreatingSystemUnderTest : WithNunit.SpecificationFor<AutofacNSubstituteTestLifetimeScope>
    {
        private ConcreteObjectWithMultipleConstructors _result;

        protected override void CreateSut()
        {
            this.SUT = new AutofacNSubstituteTestLifetimeScope();
        }

        protected void When_creating_SUT_for_object_with_multiple_constructors()
        {
            this._result = this.SUT.SystemUnderTest<ConcreteObjectWithMultipleConstructors>();
        }

        public void Then_should_create_SUT()
        {
            this._result.Should().BeOfType<ConcreteObjectWithMultipleConstructors>();
        }

        public void AndThen_should_create_SUT_using_the_greediest_constructor()
        {
            this._result.Dependency1.Should().NotBeNull();
            this._result.Dependency2.Should().NotBeNull();
        }

        public void AndThen_SUT_should_be_a_singleton()
        {
            this._result.Should().BeSameAs(this.SUT.SystemUnderTest<ConcreteObjectWithMultipleConstructors>());
        }

        public void AndThen_SUT_dependencies_should_be_singletons()
        {
            this.SUT.Resolve<IDependency1>().Should().BeSameAs(this.SUT.Resolve<IDependency1>());
            this.SUT.Resolve<IDependency2>().Should().BeSameAs(this.SUT.Resolve<IDependency2>());
        }

        [AndThen("And non-SUT dependencies should be singletons")]
        public void AndThen_non_SUT_dependencies_should_be_singletons()
        {
            this.SUT.Resolve<IDependency3>().Should().BeSameAs(this.SUT.Resolve<IDependency3>());
        }
    }

    internal class CreatingSystemUnderTestWithNoConstructor : WithNunit.SpecificationFor<AutofacNSubstituteTestLifetimeScope>
    {
        private ConcreteObjectWithNoConstructor _result;

        protected override void CreateSut()
        {
            this.SUT = new AutofacNSubstituteTestLifetimeScope();
        }

        protected void When_creating_SUT_for_object_with_no_constructor()
        {
            this._result = this.SUT.SystemUnderTest<ConcreteObjectWithNoConstructor>();
        }

        public void Then_should_create_SUT()
        {
            this._result.Should().BeOfType<ConcreteObjectWithNoConstructor>();
        }
    }

    internal class CanInjectSutDependenciesBeforeSutCreation : WithNunit.SpecificationFor<AutofacNSubstituteTestLifetimeScope>
    {
        private ConcreteObjectWithMultipleConstructors _result;

        protected override void CreateSut()
        {
            this.SUT = new AutofacNSubstituteTestLifetimeScope();
        }

        public void Given_a_constructor_dependency_has_been_provided()
        {
            this.SUT.RegisterInstance<IDependency1>(new Dependency1 { Value = 15 });
        }

        protected void When_creating_SUT()
        {
            this._result = this.SUT.SystemUnderTest<ConcreteObjectWithMultipleConstructors>();
        }

        public void Then_SUT_will_have_the_provided_dependency()
        {
            //_result.Dependency1.Value.Should().Be(15);
            throw new NotImplementedException();
        }
    }

    [TestFixture]
    public class AutofacNSubstituteTestLifetimeScopeSpecs
    {
        [Test]
        [Ignore("Replaced by Specify spec")]
        public void should_be_able_to_inject_dependencies_into_sut_before_sut_creation()
        {
            var sut = new AutofacNSubstituteTestLifetimeScope();
            sut.RegisterInstance<IDependency1>(new Dependency1 { Value = 15 });

            var result = sut.SystemUnderTest<ConcreteObjectWithMultipleConstructors>();

            result.Dependency1.Value.Should().Be(15);
        }

        [Test]
        [Ignore("Replaced by Specify spec")]
        public void should_create_sut_with_no_constructor()
        {
            var sut = new AutofacNSubstituteTestLifetimeScope();
            sut.SystemUnderTest<ConcreteObjectWithNoConstructor>()
                .Should().BeOfType<ConcreteObjectWithNoConstructor>();
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
        [Ignore("Replaced by Specify spec")]
        public void sut_dependency_should_be_a_singleton()
        {
            var sut = new AutofacNSubstituteTestLifetimeScope();
            var expected = sut.Resolve<IDependency2>();
            var result = sut.Resolve<IDependency2>();
            result.Should().BeSameAs(expected);
        }

        [Test]
        [Ignore("Replaced by Specify spec")]
        public void sut_should_be_a_singleton()
        {
            var sut = new AutofacNSubstituteTestLifetimeScope();
            var expected = sut.SystemUnderTest<ConcreteObjectWithNoConstructor>();
            var result = sut.SystemUnderTest<ConcreteObjectWithNoConstructor>();
            result.Should().BeSameAs(expected);
        }

        [Test]
        [Ignore("Replaced by Specify spec")]
        public void should_create_sut_with_greediest_constructor()
        {
            var sut = new AutofacNSubstituteTestLifetimeScope();
            var result = sut.SystemUnderTest<ConcreteObjectWithMultipleConstructors>();

            result.Should().BeOfType<ConcreteObjectWithMultipleConstructors>();
            result.Dependency1.Should().NotBeNull();
            result.Dependency2.Should().NotBeNull();
        }

        [Test]
        [Ignore("Replaced by Specify spec")]
        public void non_sut_dependency_should_be_a_singleton()
        {
            var sut = new AutofacNSubstituteTestLifetimeScope();
            var expected = sut.Resolve<IDependency3>();

            var result = sut.Resolve<IDependency3>();

            result.Should().BeSameAs(expected);
        }
    }
}
