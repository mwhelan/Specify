using NUnit.Framework;
using Specify.Stories;
using Specs.Library.Helpers;

namespace Specs.Integration.ApiTemplate
{
    /// <summary>
    /// The base class for scenarios without a story (normally unit test scenarios).
    /// </summary>
    /// <typeparam name="TSut">The type of the t sut.</typeparam>
    [TestFixture]
    public abstract class ScenarioFor<TSut> : Specify.ScenarioFor<TSut>
        where TSut : class
    {
        [ExecuteScenario]
        public override void Specify()
        {
            base.Specify();
        }
    }

    /// <summary>
    /// The base class for scenarios with a story (BDD-style acceptance tests).
    /// </summary>
    /// <typeparam name="TSut">The type of the SUT.</typeparam>
    /// <typeparam name="TStory">The type of the t story.</typeparam>
    [TestFixture]
    public abstract class ScenarioFor<TSut, TStory> : Specify.ScenarioFor<TSut, TStory>
        where TSut : class
        where TStory : Story, new()
    {
        [ExecuteScenario]
        public override void Specify()
        {
            base.Specify();
        }
    }
}