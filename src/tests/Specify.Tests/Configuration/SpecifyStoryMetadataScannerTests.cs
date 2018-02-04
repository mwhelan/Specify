using System.Collections.Generic;
using System.Linq;
using NSubstitute;
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
        private readonly SpecifyStoryMetadataScanner _sut = new SpecifyStoryMetadataScanner();
        private StoryMetadata _result;

        [Test]
        public void should_return_null_if_scanned_object_is_not_a_specification()
        {
            _result = _sut.Scan(new ConcreteObjectWithMultipleConstructors(new Dependency1()));
            _result.ShouldBe(null);
        }

        [Test]
        public void Unit_Story_should_have_sut_for_title_and_custom_title_prefix()
        {
            _result = _sut.Scan(new StubUnitScenario());
            _result.Title.ShouldBe("ConcreteObjectWithMultipleConstructors");
            _result.TitlePrefix.ShouldBe("Specifications For: ");
        }

        [Test]
        public void Unit_Story_should_be_able_to_customise_sut_title_for_report()
        {
            _result = _sut.Scan(new StubUnitScenarioWithCustomTitle());
            var title = _result.Title;
            title.ShouldBe("Custom title");
        }

        [Test]
        public void Unit_Story_should_have_sut_for_story_type()
        {
            _result = _sut.Scan(new StubUnitScenario());
            _result.Type.ShouldBe(typeof(ConcreteObjectWithMultipleConstructors));
        }

        [Test]
        public void User_Story_should_have_Humanized_story_class_name_as_title_if_no_title_specified()
        {
            _result = _sut.Scan(new StubUserStoryScenario());
            _result.Title.ShouldBe("Withdraw cash user story");
        }

        [Test]
        public void Value_Story_should_have_specified_story_title()
        {
            _result = _sut.Scan(new StubValueStoryScenario());
            _result.Title.ShouldBe("Tic Tac Toe Story");
        }

        [Test]
        public void User_Story_should_have_standard_title_prefix_if_none_specified()
        {
            _result = _sut.Scan(new StubUserStoryScenario());
            _result.TitlePrefix.ShouldBe("Story: ");
        }

        [Test]
        public void Value_Story_should_have_specified_title_prefix()
        {
            _result = _sut.Scan(new StubValueStoryScenario());
            _result.TitlePrefix.ShouldBe("User Story 1:");
        }
        [Test]
        public void should_use_story_attribute_if_present()
        {
            _result = _sut.Scan(new StubUserStoryScenarioForWithStoryAttribute());
            _result.Title.ShouldBe("Title from attribute");
            _result.TitlePrefix.ShouldBe("Title prefix from attribute");
            _result.Narrative1.ShouldBe("As a programmer");
            _result.Narrative2.ShouldBe("I want to be able to explicitly specify a story");
            _result.Narrative3.ShouldBe("So that I can share a story definition between several scenarios");
            _result.ImageUri.ShouldBe("http://www.google.co.uk");
            _result.StoryUri.ShouldBe("http://www.bbc.co.uk");
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
