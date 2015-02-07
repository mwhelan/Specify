using System;
using Shouldly;
using Specify.Tests.Stubs;
using Specify.WithAutofacNSubstitute;
using TestStack.BDDfy;

namespace Specify.Tests.WithAutofacNSubstitute
{
    public class CreatingSystemUnderTest : SpecificationFor<AutofacNSubstituteTestLifetimeScope>
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

    internal class CreatingSystemUnderTestWithNoConstructor : SpecificationFor<AutofacNSubstituteTestLifetimeScope>
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

    internal class CanInjectSutDependenciesBeforeSutCreation : SpecificationFor<AutofacNSubstituteTestLifetimeScope>
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
}
