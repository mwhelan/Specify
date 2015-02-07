using NUnit.Framework;

namespace Specify.Examples.Nunit
{
    [TestFixture]
    public abstract class ScenarioFor<TSut> : Specify.ScenarioFor<TSut> where TSut : class
    {
        [Test]
        public override void ExecuteTest()
        {
            base.ExecuteTest();
        }
    }

    [TestFixture]
    public abstract class ScenarioFor<TSut, TStory> : Specify.ScenarioFor<TSut, TStory>
        where TSut : class
        where TStory : UserStory
    {
        [Test]
        public override void ExecuteTest()
        {
            base.ExecuteTest();
        }
    }

}