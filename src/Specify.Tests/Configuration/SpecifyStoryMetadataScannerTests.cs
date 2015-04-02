using NUnit.Framework;
using Shouldly;
using Specify.Configuration;
using Specify.Tests.Stubs;

namespace Specify.Tests.Configuration
{
    [TestFixture]
    public class SpecifyStoryMetadataScannerTests
    {
        [Test]
        public void should_return_null_if_scanned_object_is_not_a_specification()
        {
            var sut = new SpecifyStoryMetadataScanner();
            var result = sut.Scan(new ConcreteObjectWithMultipleConstructors());
            result.ShouldBe(null);
        }

        [Test]
        public void Specification_should_have_sut_for_title_and_custom_title_prefix()
        {
            var sut = new SpecifyStoryMetadataScanner();
            var result = sut.Scan(new StubUnitScenario());
            result.Title.ShouldBe("ConcreteObjectWithMultipleConstructors");
            result.TitlePrefix.ShouldBe("Specifications For: ");
        }

        [Test]
        public void Scenario_should_have_Humanized_class_name_as_title()
        {
            var sut = new SpecifyStoryMetadataScanner();
            var result = sut.Scan(new StubUserStoryScenario());
            result.Title.ShouldBe("Stub User Story Scenario");
        }

        [Test]
        public void Scenario_should_have_standard_title_prefix()
        {
            var sut = new SpecifyStoryMetadataScanner();
            var result = sut.Scan(new StubUserStoryScenario());
            result.TitlePrefix.ShouldBe("Story: ");
        }

        [Test]
        public void Scenario_title_should_include_number_if_it_has_been_set()
        {
            var sut = new SpecifyStoryMetadataScanner();
            var result = sut.Scan(new StubUserStoryScenario {Number = 3});
            result.Title.ShouldBe("Scenario 03: Stub User Story Scenario");
        }

        [Test]
        public void should_use_story_attribute_if_present()
        {
            var sut = new SpecifyStoryMetadataScanner();
            var result = sut.Scan(new StubUserStoryScenarioForWithStoryAttribute());
            result.Title.ShouldBe("Title from attribute");
            result.TitlePrefix.ShouldBe("Title prefix from attribute");
            result.Narrative1.ShouldBe("As a programmer");
            result.Narrative2.ShouldBe("I want to be able to explicitly specify a story");
            result.Narrative3.ShouldBe("So that I can share a story definition between several scenarios");
        }

    }
}
