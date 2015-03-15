using NUnit.Framework;
using Shouldly;
using Specify.Configuration;
using Specify.Tests.Stubs;

namespace Specify.Tests.Tests
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
            var result = sut.Scan(new StubSpecificationFor());
            result.Title.ShouldBe("ConcreteObjectWithMultipleConstructors");
            result.TitlePrefix.ShouldBe("Specifications For: ");
        }

        [Test]
        public void Scenario_should_have_Humanized_class_name_as_title()
        {
            var sut = new SpecifyStoryMetadataScanner();
            var result = sut.Scan(new StubScenarioFor());
            result.Title.ShouldBe("Stub Scenario For");
        }

        [Test]
        public void Scenario_should_have_standard_title_prefix()
        {
            var sut = new SpecifyStoryMetadataScanner();
            var result = sut.Scan(new StubScenarioFor());
            result.TitlePrefix.ShouldBe("Story: ");
        }

        [Test]
        public void Scenario_title_should_include_number_if_it_has_been_set()
        {
            var sut = new SpecifyStoryMetadataScanner();
            var result = sut.Scan(new StubScenarioFor {Number = 3});
            result.Title.ShouldBe("Scenario 03: Stub Scenario For");
        }
    }
}
