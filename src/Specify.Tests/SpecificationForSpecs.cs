using FluentAssertions;
using NUnit.Framework;
using Specify.Containers;
using Specify.Tests.Stubs;

namespace Specify.Tests
{
    public class SpecificationForSpecs
    {
        [Test]
        public void Resolver_should_be_an_auto_mocking_container()
        {
            var sut = new SpecWithAllSupportedStepsInRandomOrder();
            sut.Resolver.Should().BeOfType<AutoMockingContainer<object>>();
        }
    }
}