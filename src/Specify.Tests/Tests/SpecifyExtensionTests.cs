using NUnit.Framework;
using Shouldly;
using Specify.Tests.Stubs;

namespace Specify.Tests.Tests
{
    [TestFixture]
    public class SpecifyExtensionTests
    {
        [Test]
        public void IsSpecificationFor_should_return_true_if_SpecificationFor()
        {
            new StubSpecificationFor()
                .IsSpecificationFor()
                .ShouldBe(true);
        }

        [Test]
        public void IsSpecificationFor_should_return_false_if_not_SpecificationFor()
        {
            new StubScenarioFor()
                .IsSpecificationFor()
                .ShouldBe(false);;
        }

        [Test]
        public void IsScenarioFor_should_return_true_if_ScenarioFor()
        {
            new StubScenarioFor()
                .IsScenarioFor()
                .ShouldBe(true);
        }

        [Test]
        public void IsScenarioFor_should_return_false_if__not_ScenarioFor()
        {
            new StubSpecificationFor()
                .IsScenarioFor()
                .ShouldBe(false);;
        }
    }
}
