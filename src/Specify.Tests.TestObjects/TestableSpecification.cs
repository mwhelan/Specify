using System;
using System.Collections.Generic;
using Specify.Containers;
using Specify.Core;

namespace Specify.Tests.TestObjects
{
    public abstract class TestableSpecificationFor<TSut> : Specification<TSut, AutoSubResolver<TSut>> where TSut : class
    {
        private List<string> _steps = new List<string>();
        public List<string> Steps
        {
            get { return _steps; }
        }

        protected TestableSpecificationFor() : base(new AutoSubResolver<TSut>()) { }

        public override void EstablishContext()
        {
            _steps.Add("Specification.EstablishContext");
            InitialiseSystemUnderTest();
        }

        public void TearDown()
        {
            _steps.Add("Specification.TearDown");
            Container.Dispose();
        }

        public override void InitialiseSystemUnderTest()
        {
            _steps.Add("Specification.InitialiseSystemUnderTest");
            SUT = Container.Resolve<TSut>();
        }
    }
}