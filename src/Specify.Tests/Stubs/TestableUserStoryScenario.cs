using System.Collections.Generic;

namespace Specify.Tests.Stubs
{
    abstract class TestableUserStoryScenario<TSut> : ScenarioFor<TSut, WithdrawCashUserStory> where TSut : class
    {
        private List<string> _steps = new List<string>();

        public List<string> Steps => _steps;
    }
}