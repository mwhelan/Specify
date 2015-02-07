using NSubstitute;
using Shouldly;
using Specify.Tests.Stubs;
using TestStack.BDDfy;

namespace Specify.Tests
{
    public class SpecificationForSpecs
    {
        internal class specification_step_order_should_follow_standard_BDDfy_conventions
            : WithNunit.SpecificationFor<SpecWithAllSupportedStepsInRandomOrder>
        {
            protected override void CreateSystemUnderTest()
            {
                SUT = new SpecWithAllSupportedStepsInRandomOrder{Container = Substitute.For<ITestLifetimeScope>()};
            }

            public void When_the_specification_is_run()
            {
                SUT.BDDfy();
            }

            public void Then_first_ConfigureContainer_is_called()
            {
                SUT.Steps[0].ShouldBe("Implementation - Constructor");
                SUT.Steps[1].ShouldBe("ConfigureContainer");
            }

            public void AndThen_CreateSystemUnderTest()
            {
                SUT.Steps[2].ShouldBe("CreateSystemUnderTest");
            }

            public void AndThen_Setup()
            {
                SUT.Steps[3].ShouldBe("Implementation - Setup");
            }

            public void AndThen_the_Givens()
            {
                SUT.Steps[4].ShouldBe("Implementation - GivenSomePrecondition");
                SUT.Steps[5].ShouldBe("Implementation - AndGivenSomeOtherPrecondition");
            }

            public void AndThen_the_Whens()
            {
                SUT.Steps[6].ShouldBe("Implementation - WhenAction");
                SUT.Steps[7].ShouldBe("Implementation - AndWhenAnotherAction");
            }

            public void AndThen_the_Thens()
            {
                SUT.Steps[8].ShouldBe("Implementation - ThenAnExpectation");
                SUT.Steps[9].ShouldBe("Implementation - AndThenAnotherExpectation");
            }

            public void AndThen_finally_TearDown()
            {
                SUT.Steps[10].ShouldBe("Implementation - TearDown");
            }
        }


        //[Test]
        //public void Resolver_should_be_an_auto_mocking_container()
        //{
        //    var sut = new SpecWithAllSupportedStepsInRandomOrder();
        //    sut.Resolver.ShouldBeOfType<AutoMockingContainer<object>>();
        //}
    }
}