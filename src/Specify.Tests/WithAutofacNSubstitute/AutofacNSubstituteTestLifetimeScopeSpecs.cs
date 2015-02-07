using System;
using NUnit.Framework;
using Shouldly;
using Specify.Tests.Stubs;
using Specify.WithAutofacNSubstitute;
using TestStack.BDDfy;

namespace Specify.Tests.WithAutofacNSubstitute
{
    public class CreatingSystemUnderTest : WithNunit.SpecificationFor<AutofacNSubstituteTestLifetimeScope>
    {
        private ConcreteObjectWithMultipleConstructors _result;

        protected override void CreateSystemUnderTest()
        {
            SUT = new AutofacNSubstituteTestLifetimeScope();
        }

        protected void When_creating_SUT_for_object_with_multiple_constructors()
        {
            _result = SUT.SystemUnderTest<ConcreteObjectWithMultipleConstructors>();
        }

        public void Then_should_create_SUT()
        {
            _result.ShouldBeOfType<ConcreteObjectWithMultipleConstructors>();
        }

        public void AndThen_should_create_SUT_using_the_greediest_constructor()
        {
            _result.Dependency1.ShouldNotBe(null);
            _result.Dependency2.ShouldNotBe(null);
        }

        public void AndThen_SUT_should_be_a_singleton()
        {
            _result.ShouldBeSameAs(SUT.SystemUnderTest<ConcreteObjectWithMultipleConstructors>());
        }

        public void AndThen_SUT_dependencies_should_be_singletons()
        {
            SUT.Resolve<IDependency1>().ShouldBeSameAs(SUT.Resolve<IDependency1>());
            SUT.Resolve<IDependency2>().ShouldBeSameAs(SUT.Resolve<IDependency2>());
        }

        [AndThen("And non-SUT dependencies should be singletons")]
        public void AndThen_non_SUT_dependencies_should_be_singletons()
        {
            SUT.Resolve<IDependency3>().ShouldBeSameAs(SUT.Resolve<IDependency3>());
        }
    }

    internal class CreatingSystemUnderTestWithNoConstructor : WithNunit.SpecificationFor<AutofacNSubstituteTestLifetimeScope>
    {
        private ConcreteObjectWithNoConstructor _result;

        protected override void CreateSystemUnderTest()
        {
            SUT = new AutofacNSubstituteTestLifetimeScope();
        }

        protected void When_creating_SUT_for_object_with_no_constructor()
        {
            _result = SUT.SystemUnderTest<ConcreteObjectWithNoConstructor>();
        }

        public void Then_should_create_SUT()
        {
            _result.ShouldBeOfType<ConcreteObjectWithNoConstructor>();
        }
    }

    internal class CanInjectSutDependenciesBeforeSutCreation : WithNunit.SpecificationFor<AutofacNSubstituteTestLifetimeScope>
    {
        private ConcreteObjectWithMultipleConstructors _result;

        protected override void CreateSystemUnderTest()
        {
            SUT = new AutofacNSubstituteTestLifetimeScope();
        }

        public void Given_a_constructor_dependency_has_been_provided()
        {
            SUT.RegisterInstance<IDependency1>(new Dependency1 { Value = 15 });
        }

        protected void When_creating_SUT()
        {
            _result = SUT.SystemUnderTest<ConcreteObjectWithMultipleConstructors>();
        }

        public void Then_SUT_will_have_the_provided_dependency()
        {
            //_result.Dependency1.Value.ShouldBe(15);
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

            result.Dependency1.Value.ShouldBe(15);
        }

        [Test]
        [Ignore("Replaced by Specify spec")]
        public void should_create_sut_with_no_constructor()
        {
            var sut = new AutofacNSubstituteTestLifetimeScope();
            sut.SystemUnderTest<ConcreteObjectWithNoConstructor>()
                .ShouldBeOfType<ConcreteObjectWithNoConstructor>();
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
            result.ShouldBeSameAs(expected);
        }

        [Test]
        [Ignore("Replaced by Specify spec")]
        public void sut_should_be_a_singleton()
        {
            var sut = new AutofacNSubstituteTestLifetimeScope();
            var expected = sut.SystemUnderTest<ConcreteObjectWithNoConstructor>();
            var result = sut.SystemUnderTest<ConcreteObjectWithNoConstructor>();
            result.ShouldBeSameAs(expected);
        }

        [Test]
        [Ignore("Replaced by Specify spec")]
        public void should_create_sut_with_greediest_constructor()
        {
            var sut = new AutofacNSubstituteTestLifetimeScope();
            var result = sut.SystemUnderTest<ConcreteObjectWithMultipleConstructors>();

            result.ShouldBeOfType<ConcreteObjectWithMultipleConstructors>();
            result.Dependency1.ShouldNotBe(null);
            result.Dependency2.ShouldNotBe(null);
        }

        [Test]
        [Ignore("Replaced by Specify spec")]
        public void non_sut_dependency_should_be_a_singleton()
        {
            var sut = new AutofacNSubstituteTestLifetimeScope();
            var expected = sut.Resolve<IDependency3>();

            var result = sut.Resolve<IDependency3>();

            result.ShouldBeSameAs(expected);
        }
    }
}
