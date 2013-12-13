using System;
using System.Linq;
using Specify.Core;
using TestStack.BDDfy.Core;
using TestStack.BDDfy.Scanners;

namespace Specify.Scanners
{
    public class ScenarioStoryMetaDataScanner : IStoryMetaDataScanner
    {
        public virtual StoryMetaData Scan(object testObject, Type explicityStoryType = null)
        {
            var scenario = testObject as ISpecification;
            if (scenario == null)
                return null;

            var storyAttribute = GetStoryAttribute(scenario.Story);
            if (storyAttribute == null)
                return null;

            return new StoryMetaData(scenario.Story, storyAttribute);
        }

        static StoryAttribute GetStoryAttribute(Type candidateStoryType)
        {
            return (StoryAttribute)candidateStoryType.GetCustomAttributes(typeof(StoryAttribute), true).FirstOrDefault();
        }
    }
}
