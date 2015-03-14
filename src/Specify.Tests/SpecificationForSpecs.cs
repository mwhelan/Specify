using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Specify.Containers;
using Specify.Tests.Stubs;
using TestStack.BDDfy;

namespace Specify.Tests
{
    [TestFixture]
    public class SpecificationForTests
    {
        [Test]
        public void specification_step_order_should_follow_standard_BDDfy_conventions()
        {
            var container = new SutFactory(Substitute.For<IContainer>());
            var sut = new SpecWithAllSupportedStepsInRandomOrder {Container = container};
         
            sut.BDDfy();

            sut.Steps[0].ShouldBe("Implementation - Constructor");
            sut.Steps[1].ShouldBe("ConfigureContainer");
            sut.Steps[2].ShouldBe("CreateSystemUnderTest");
            sut.Steps[3].ShouldBe("Implementation - Setup");
            sut.Steps[4].ShouldBe("Implementation - GivenSomePrecondition");
            sut.Steps[5].ShouldBe("Implementation - AndGivenSomeOtherPrecondition");
            sut.Steps[6].ShouldBe("Implementation - WhenAction");
            sut.Steps[7].ShouldBe("Implementation - AndWhenAnotherAction");
            sut.Steps[8].ShouldBe("Implementation - ThenAnExpectation");
            sut.Steps[9].ShouldBe("Implementation - AndThenAnotherExpectation");
            sut.Steps[10].ShouldBe("Implementation - TearDown");
        }


        //[Test]
        //public void Resolver_should_be_an_auto_mocking_container()
        //{
        //    var sut = new SpecWithAllSupportedStepsInRandomOrder();
        //    sut.Resolver.ShouldBeOfType<AutoMockingContainer<object>>();
        //}
    }
}