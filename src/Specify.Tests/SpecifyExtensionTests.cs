using FluentAssertions;
using NUnit.Framework;
using Specify.Tests.Stubs;

namespace Specify.Tests
{
    public class SpecifyExtensionTests
    {
        [Test]
        public void IsSpecificationFor_should_return_true_if_SpecificationFor()
        {
            new StubSpecificationFor()
                .IsSpecificationFor()
                .Should().BeTrue();
        }

        [Test]
        public void IsSpecificationFor_should_return_false_if_not_SpecificationFor()
        {
            new StubScenarioFor()
                .IsSpecificationFor()
                .Should().BeFalse();
        }

        [Test]
        public void IsScenarioFor_should_return_true_if_ScenarioFor()
        {
            new StubScenarioFor()
                .IsScenarioFor()
                .Should().BeTrue();
        }

        [Test]
        public void IsScenarioFor_should_return_false_if__not_ScenarioFor()
        {
            new StubSpecificationFor()
                .IsScenarioFor()
                .Should().BeFalse();
        }
    }
}
