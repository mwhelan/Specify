using FluentAssertions;
using NUnit.Framework;
using Specify.Configuration;
using Specify.Tests.Stubs;

namespace Specify.Tests.Configuration
{
    public class SpecifyStoryMetadataScannerSpecs
    {
        [Test]
        public void should_return_null_if_scanned_object_is_not_a_specification()
        {
            var sut = new SpecifyStoryMetadataScanner();
            var result = sut.Scan(new ConcreteObjectWithMultipleConstructors());
            result.Should().BeNull();
        }

        [Test]
        public void Specification_should_have_sut_for_title_and_custom_title_prefix()
        {
            var sut = new SpecifyStoryMetadataScanner();
            var result = sut.Scan(new StubSpecificationFor());
            result.Title.Should().Be("ConcreteObjectWithMultipleConstructors");
            result.TitlePrefix.Should().Be("Specifications For: ");
        }

        [Test]
        public void Scenario_should_have_Humanized_class_name_as_title()
        {
            var sut = new SpecifyStoryMetadataScanner();
            var result = sut.Scan(new StubScenarioFor());
            result.Title.Should().Be("Stub Scenario For");
        }

        [Test]
        public void Scenario_should_have_standard_title_prefix()
        {
            var sut = new SpecifyStoryMetadataScanner();
            var result = sut.Scan(new StubScenarioFor());
            result.TitlePrefix.Should().Be("Story: ");
        }

        [Test]
        public void Scenario_title_should_include_number_if_it_has_been_set()
        {
            var sut = new SpecifyStoryMetadataScanner();
            var result = sut.Scan(new StubScenarioFor{ScenarioNumber = 3});
            result.Title.Should().Be("Scenario 03: Stub Scenario For");
        }
    }
}
