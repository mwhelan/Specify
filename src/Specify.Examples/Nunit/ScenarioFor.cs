using NUnit.Framework;
using Specify.Stories;

namespace Specify.Examples.Nunit
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
    }

    [TestFixture]
    public abstract class ScenarioFor<TSut> : Specify.ScenarioFor<TSut> 
        where TSut : class
    {
        [Test]
        public override void Specify()
        {
            base.Specify();
        }
    }
}
