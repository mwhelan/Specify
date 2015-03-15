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

            sut.Steps[0].ShouldBe("Constructor");
            sut.Steps[1].ShouldBe("Setup");
            sut.Steps[2].ShouldBe("EstablishContext");
            sut.Steps[3].ShouldBe("GivenSomePrecondition");
            sut.Steps[4].ShouldBe("AndGivenSomeOtherPrecondition");
            sut.Steps[5].ShouldBe("WhenAction");
            sut.Steps[6].ShouldBe("AndWhenAnotherAction");
            sut.Steps[7].ShouldBe("ThenAnExpectation");
            sut.Steps[8].ShouldBe("AndThenAnotherExpectation");
            sut.Steps[9].ShouldBe("TearDown");
        }

        //[Test]
        //public void Resolver_should_be_an_auto_mocking_container()
        //{
        //    var sut = new SpecWithAllSupportedStepsInRandomOrder();
        //    sut.Resolver.ShouldBeOfType<AutoMockingContainer<object>>();
        //}
    }
}