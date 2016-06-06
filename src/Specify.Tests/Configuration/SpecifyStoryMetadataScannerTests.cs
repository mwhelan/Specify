using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Specify.Configuration;
using Specify.Tests.Stubs;
using TestStack.BDDfy;
using TestStack.BDDfy.Reporters;

namespace Specify.Tests.Configuration
{
    [TestFixture]
    public class SpecifyStoryMetadataScannerTests
    {
        [Test]
        public void should_return_null_if_scanned_object_is_not_a_specification()
        {
            var sut = new SpecifyStoryMetadataScanner();
            var result = sut.Scan(new ConcreteObjectWithMultipleConstructors(new Dependency1()));
            result.ShouldBe(null);
        }

        [Test]
        public void Unit_Story_should_have_sut_for_title_and_custom_title_prefix()
        {
            var sut = new SpecifyStoryMetadataScanner();
            var result = sut.Scan(new StubUnitScenario());
            result.Title.ShouldBe("ConcreteObjectWithMultipleConstructors");
            result.TitlePrefix.ShouldBe("Specifications For: ");
        }

        [Test]
        public void Unit_Story_should_have_sut_for_story_type()
        {
            var sut = new SpecifyStoryMetadataScanner();
            var result = sut.Scan(new StubUnitScenario());
            result.Type.ShouldBe(typeof(ConcreteObjectWithMultipleConstructors));
        }

        [Test]
        public void User_Story_should_have_Humanized_story_class_name_as_title_if_no_title_specified()
        {
            var sut = new SpecifyStoryMetadataScanner();
            var result = sut.Scan(new StubUserStoryScenario());
            result.Title.ShouldBe("Withdraw cash user story");
        }

        [Test]
        public void Value_Story_should_have_specified_story_title()
        {
            var sut = new SpecifyStoryMetadataScanner();
            var result = sut.Scan(new StubValueStoryScenario());
            result.Title.ShouldBe("Tic Tac Toe Story");
        }

        [Test]
        public void User_Story_should_have_standard_title_prefix_if_none_specified()
        {
            var sut = new SpecifyStoryMetadataScanner();
            var result = sut.Scan(new StubUserStoryScenario());
            result.TitlePrefix.ShouldBe("Story: ");
        }

        [Test]
        public void Value_Story_should_have_specified_title_prefix()
        {
            var sut = new SpecifyStoryMetadataScanner();
            var result = sut.Scan(new StubValueStoryScenario());
            result.TitlePrefix.ShouldBe("User Story 1:");
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

        [Test]
        public void should_work_correctly_with_BDDfy_file_report_model()
        {
            var fileReportModel = CreateReportModelWithTwoStoriesThreeScenarios();
            fileReportModel.Stories.ToList().Count.ShouldBe(2);
        }

        private static FileReportModel CreateReportModelWithTwoStoriesThreeScenarios()
        {
            var stories = new List<Story>
            {
                CreateBDDfyStory(new StubUnitScenario()),
                CreateBDDfyStory(new ConcreteObjectWithNoConstructorUnitScenario()),
                CreateBDDfyStory(new UnitScenarioWithAllSupportedStepsInRandomOrder())
            };

            return new FileReportModel(stories.ToReportModel());
        }
        private static Story CreateBDDfyStory(IScenario scenario)
        {
            var story = scenario.BDDfy();
            var metadata = new SpecifyStoryMetadataScanner().Scan(scenario);
            return new Story(metadata, story.Scenarios.ToArray());
        }
    }
}
