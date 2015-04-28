using NUnit.Framework;
using Specify.Stories;

namespace ContosoUniversity.SubcutaneousTests
{
    [TestFixture]
    public abstract class ScenarioFor<TSut, TStory> : Specify.ScenarioFor<TSut, TStory>
        where TSut : class
        where TStory : Story, new()
    {
        [Test]
        public override void Specify()
        {
            base.Specify();
        }

        public override string Title
        {
            get
            {
                const string scenarioSuffix = " Scenario";
                var title = base.Title;
                if (title.EndsWith(scenarioSuffix))
                {
                    return title.Replace(scenarioSuffix, string.Empty);
                }
                return title;
            }
        }
    }
}