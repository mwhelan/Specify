using System.Collections.Generic;

namespace Specify.Tests.Stubs
{
    abstract class TestableSpecification<TSut> : SpecificationFor<TSut> where TSut : class
    {
        private List<string> _steps = new List<string>();

        public TestableSpecification()
        {
          //  Context = new SpecificationContext<TSut>(Substitute.For<ITestLifetimeScope>());
        }
        public List<string> Steps
        {
            get { return _steps; }
        }

        //protected override void EstablishContext()
        //{
        //    _steps.Add("SPECIFICATION - EstablishContext");
        //}
    }
}