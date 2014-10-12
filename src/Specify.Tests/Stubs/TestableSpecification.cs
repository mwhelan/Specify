using System.Collections.Generic;
using Specify.Containers;

namespace Specify.Tests.Stubs
{
    abstract class TestableSpecification<TSut> : Specification<TSut, AutoMockingContainer<TSut>> where TSut : class
    {
        private List<string> _steps = new List<string>();

        public TestableSpecification()
        {
            Resolver = new AutoMockingContainer<TSut>();
        }
        public List<string> Steps
        {
            get { return _steps; }
        }

        protected override void EstablishContext()
        {
            _steps.Add("SPECIFICATION - EstablishContext");
        }
    }
}