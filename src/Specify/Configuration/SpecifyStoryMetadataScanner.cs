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
            var scenario = testObject as IScenario;
            if (scenario == null)
                return null;

            return scenario.IsStoryScenario() 
                ? CreateScenarioMetadata(scenario) 
                : CreateSpecificationMetadata(scenario);
        }

        private StoryMetadata CreateScenarioMetadata(IScenario scenario)
        {
            var storyAttribute = (StoryNarrativeAttribute)scenario.GetType()
                .GetCustomAttributes(typeof(StoryNarrativeAttribute), true)
                .FirstOrDefault();

            if (storyAttribute != null)
            {
                return new StoryMetadata(scenario.Story, storyAttribute);
            }

            var story = scenario.Story.Create<Story>(); 
            return new StoryMetadata(scenario.Story, story.Narrative1, story.Narrative2,
                story.Narrative3, story.Title, story.TitlePrefix);
        }

        private StoryMetadata CreateSpecificationMetadata(IScenario specification)
        {
            var title = specification.GetType().GetProperty("SUT").PropertyType.Name;
            var story = specification.Story.Create<Story>();
            var storyAttribute = new StoryAttribute() { Title = title, TitlePrefix = story.TitlePrefix };
            return new StoryMetadata(specification.Story, storyAttribute);
        }
    }
}
