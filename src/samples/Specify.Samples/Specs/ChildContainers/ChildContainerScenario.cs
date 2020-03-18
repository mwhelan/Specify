using Shouldly;
using Specify.Samples.Domain.ChildContainers;

namespace Specify.Samples.Specs.ChildContainers
{
    public class ChildContainerScenario : ScenarioFor<ConcreteObject>
    {
        private IDependency1 _result;

        public override void RegisterContainerOverrides()
        {
            TestScope.Set<IDependency1, TestDependency1>();
        }

        public void Given_I_have_overridden_the_registrations_in_the_TestScope()
        {
            // this happens in the RegisterContainerOverrides method
        }

        public void When_I_exercise_the_System_Under_Test()
        {
            _result = SUT.Dependency1;
        }

        public void Then_the_SUT_should_use_the_new_dependency()
        {
            _result.ShouldBeOfType<TestDependency1>();
            _result.Value.ShouldBe(99);
        }
    }
}
