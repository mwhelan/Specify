using System.Collections.Generic;
using NSubstitute;
using Specify.Containers;

namespace Specify.Tests.Stubs
{
    abstract class TestableSpecification<TSut> : SpecificationFor<TSut> where TSut : class
    {
        private List<string> _steps = new List<string>();

        public TestableSpecification()
        {
            Scope = Substitute.For<IDependencyScope>();
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