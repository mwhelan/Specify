using Shouldly;
using Specify.Tests.TestObjects;
using TestStack.BDDfy;
using Xunit;

namespace Specify.Tests
{
    public class StepExecutionOrderSpecs
    {
        private SpecWithAllSupportedSteps SUT;

        public void Given_a_spec_with_all_supported_steps()
        {
            SUT = new SpecWithAllSupportedSteps();
        }

        public void When_the_spec_is_run()
        {
            SUT.Run();
        }

        [Then("Then the first step run is one that is named Setup [Specification]")]
        public void Then_the_first_step_run_is_one_that_is_named_Setup()
        {
            SUT.Steps[0].ShouldBe("SpecWithAllSupportedSteps.Setup");
        }

        [AndThen("And the next step is one that ends with Context [Specify]")]
        public void AndThen_the_next_step_is_run_by_Specify()
        {
            SUT.Steps[1].ShouldBe("Specification.EstablishContext");
        }

        [AndThen("And the next step is InitialiseSystemUnderTest [Specify, virtual]")]
        public void AndThen_the_next_step_is_InitialiseSystemUnderTest()
        {
            SUT.Steps[2].ShouldBe("Specification.InitialiseSystemUnderTest");
        }

        [AndThen("And then the following steps are the Givens, Whens and Thens [Specification]")]
        public void AndThen_the_following_steps_are_the_Givens_Whens_And_Thens()
        {
            SUT.Steps[3].ShouldBe("SpecWithAllSupportedSteps.GivenSomePrecondition");
            SUT.Steps[4].ShouldBe("SpecWithAllSupportedSteps.AndGivenSomeOtherPrecondition");
            SUT.Steps[5].ShouldBe("SpecWithAllSupportedSteps.WhenAction");
            SUT.Steps[6].ShouldBe("SpecWithAllSupportedSteps.AndWhenAnotherAction");
            SUT.Steps[7].ShouldBe("SpecWithAllSupportedSteps.ThenAnExpectation");
            SUT.Steps[8].ShouldBe("SpecWithAllSupportedSteps.AndThenAnotherExpectation");
        }

        [AndThen("And the last step run is TearDown [Specify, Virtual]")]
        public void AndThen_the_last_step_run()
        {
            SUT.Steps[9].ShouldBe("Specification.TearDown");
        }

        [Fact]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}