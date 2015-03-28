using System;
using System.Linq;
using TestStack.BDDfy;
using Story = Specify.Stories.Story;

namespace Specify.Configuration
{
    public class SpecifyStoryMetadataScanner : IStoryMetadataScanner
    {
        public virtual StoryMetadata Scan(object testObject, Type explicityStoryType = null)
        {
            var specification = testObject as ISpecification;
            if (specification == null)
                return null;

            return specification.IsScenarioFor() 
                ? CreateScenarioMetadata(specification) 
                : CreateSpecificationMetadata(specification);
        }

        private StoryMetadata CreateScenarioMetadata(ISpecification scenario)
        {
            var storyAttribute = (StoryNarrativeAttribute)scenario.GetType()
                .GetCustomAttributes(typeof(StoryNarrativeAttribute), true)
                .FirstOrDefault();

            if (storyAttribute != null)
            {
                return new StoryMetadata(scenario.Story, storyAttribute);
            }

            var story = (Story)Activator.CreateInstance(scenario.Story);
            return new StoryMetadata(scenario.Story, story.Narrative1, story.Narrative2,
                story.Narrative3, scenario.Title, story.TitlePrefix);
        }

        private StoryMetadata CreateSpecificationMetadata(ISpecification specification)
        {
            var story = (Story)Activator.CreateInstance(specification.Story);
            var storyAttribute = new StoryAttribute() { Title = specification.Title, TitlePrefix = story.TitlePrefix };
            return new StoryMetadata(specification.Story, storyAttribute);
        }
    }
}
