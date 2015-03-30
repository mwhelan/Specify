using System.Collections.Generic;

namespace Specify.Tests.Stubs
{
    abstract class TestableScenarioFor<TSut> : SpecificationFor<TSut, WithdrawCashUserStory> where TSut : class
    {
        private List<string> _steps = new List<string>();

        public TestableScenarioFor()
        {
           // Context = new SutFactory<TSut>(Substitute.For<ITestLifetimeScope>());
        }
        public List<string> Steps
        {
            get { return _steps; }
        }

        //protected override void EstablishContext()
        //{
        //    _steps.Add("SCENARIO - EstablishContext");
        //}
    }
}