using NUnit.Framework;
using Specify.Stories;
using Specs.Library.Data;
using Specs.Library.Helpers;

namespace Specs.Acceptance
{
    [TestFixture]
    public abstract class ScenarioFor<TSut, TStory> : Specify.ScenarioFor<TSut, TStory>
        where TSut : class
        where TStory : Story, new()
    {
         protected IDb Db => Container.Get<IDb>();

        [ExecuteScenario]
        public override void Specify()
        {
            base.Specify();
        }
    }
}