using System.Collections.Generic;

namespace Specify.Tests.Stubs
{
    abstract class TestableUnitScenario<TSut> : ScenarioFor<TSut> where TSut : class
    {
        private List<string> _steps = new List<string>();

        public List<string> Steps
        {
            get { return _steps; }
        }
    }
}