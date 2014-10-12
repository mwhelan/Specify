using System;
using System.Linq;
using TestStack.BDDfy;

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
            var storyAttribute = (StoryAttribute)scenario
                .Story
                .GetCustomAttributes(typeof(StoryAttribute), true)
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
            string specificationTitle = specification.Story.Name;// CreateSpecificationTitle(specification);
            var story = new StoryAttribute() { Title = specificationTitle, TitlePrefix = "Specifications For: " };
            return new StoryMetadata(specification.Story, story);
        }

        //private string CreateSpecificationTitle(ISpecification specification)
        //{
        //    string suffix = "Specification";
        //    string title = specification.Story.Name;
        //    if (title.EndsWith(suffix))
        //        title = title.Remove(title.Length - suffix.Length, suffix.Length);
        //    return title;
        //}

    }
}
