using System;
using Specify.Core;
using TestStack.BDDfy;

namespace Specify.Scanners
{
    public class SpecStoryMetaDataScanner : IStoryMetadataScanner
    {
        public virtual StoryMetadata Scan(object testObject, Type explicityStoryType = null)
        {
            var specification = testObject as ISpecification;
            if (specification == null)
                return null;

            string specificationTitle = CreateSpecificationTitle(specification);
            var story = new StoryAttribute() {Title = specificationTitle, TitlePrefix = "Specifications For: "};
            return new StoryMetadata(specification.Story, story);
        }

        private string CreateSpecificationTitle(ISpecification specification)
        {
            string suffix = "Specification";
            string title = specification.Story.Name;
            if (title.EndsWith(suffix))
                title = title.Remove(title.Length - suffix.Length, suffix.Length);
            return title;
        }
    }
}
