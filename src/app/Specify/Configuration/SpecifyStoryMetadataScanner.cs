using System;
using System.Linq;
using TestStack.BDDfy;
using Story = Specify.Stories.Story;

namespace Specify.Configuration
{
    /// <summary>
    /// BDDfy story metadata scanner.
    /// </summary>
    public class SpecifyStoryMetadataScanner : IStoryMetadataScanner
    {
        /// <summary>
        /// Scans the specified test object.
        /// </summary>
        /// <param name="testObject">The test object.</param>
        /// <param name="explicityStoryType">Type of the explicity story.</param>
        /// <returns>StoryMetadata.</returns>
        public virtual StoryMetadata Scan(object testObject, Type explicityStoryType = null)
        {
            if (!(testObject is IScenario scenario))
                return null;

            return scenario.IsStoryScenario() 
                ? CreateScenarioMetadata(scenario) 
                : CreateSpecificationMetadata(scenario);
        }

        // creates display for normal BDDfy-style reports
        private static StoryMetadata CreateScenarioMetadata(IScenario scenario)
        {
            var storyAttribute = (StoryNarrativeAttribute)scenario.GetType()
                .GetCustomAttributes(typeof(StoryNarrativeAttribute), true)
                .FirstOrDefault();

            if (storyAttribute != null)
            {
                return new StoryMetadata(scenario.Story.GetType(), storyAttribute);
            }

            var story = scenario.Story; 
            return new StoryMetadata(scenario.Story.GetType(), story.Narrative1, story.Narrative2,
                story.Narrative3, story.Title, story.TitlePrefix, story.ImageUri, story.StoryUri);
        }

        // creates display for unit test reports
        private static StoryMetadata CreateSpecificationMetadata(IScenario specification)
        {
            var sutType = specification.SutType();
            // scenario title on report is name of SUT class unless story title is overridden in
            var title = specification.Story.Title ?? sutType.Name;
            var story = specification.Story;
            var storyAttribute = new StoryAttribute { Title = title, TitlePrefix = story.TitlePrefix };
            return new StoryMetadata(sutType, storyAttribute);
        }
    }
}
