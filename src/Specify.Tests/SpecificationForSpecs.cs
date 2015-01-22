using FluentAssertions;
using NUnit.Framework;
using NSubstitute;
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
                SUT.Steps[0].Should().Be("Implementation - Constructor");
                SUT.Steps[1].Should().Be("ConfigureContainer");
            }

            public void AndThen_CreateSystemUnderTest()
            {
                SUT.Steps[2].Should().Be("CreateSystemUnderTest");
            }

            public void AndThen_Setup()
            {
                SUT.Steps[3].Should().Be("Implementation - Setup");
            }

            public void AndThen_the_Givens()
            {
                SUT.Steps[4].Should().Be("Implementation - GivenSomePrecondition");
                SUT.Steps[5].Should().Be("Implementation - AndGivenSomeOtherPrecondition");
            }

            public void AndThen_the_Whens()
            {
                SUT.Steps[6].Should().Be("Implementation - WhenAction");
                SUT.Steps[7].Should().Be("Implementation - AndWhenAnotherAction");
            }

            public void AndThen_the_Thens()
            {
                SUT.Steps[8].Should().Be("Implementation - ThenAnExpectation");
                SUT.Steps[9].Should().Be("Implementation - AndThenAnotherExpectation");
            }

            public void AndThen_finally_TearDown()
            {
                SUT.Steps[10].Should().Be("Implementation - TearDown");
            }
        }


        //[Test]
        //public void Resolver_should_be_an_auto_mocking_container()
        //{
        //    var sut = new SpecWithAllSupportedStepsInRandomOrder();
        //    sut.Resolver.Should().BeOfType<AutoMockingContainer<object>>();
        //}
    }
}