using System.Collections.Generic;
using NSubstitute;
using Specify.Containers;

namespace Specify.Tests.Stubs
{
    abstract class TestableScenarioFor<TSut> : ScenarioFor<TSut> where TSut : class
    {
        private List<string> _steps = new List<string>();

        public TestableScenarioFor()
        {
            Context = new SpecificationContext<TSut>(Substitute.For<IDependencyScope>());
        }
        public List<string> Steps
        {
            get { return _steps; }
        }

        protected override void EstablishContext()
        {
            _steps.Add("SCENARIO - EstablishContext");
        }
    }
}