using System.Collections.Generic;

namespace Specify.Tests.Stubs
{
    abstract class TestableSpecification<TSut> : SpecificationFor<TSut> where TSut : class
    {
        private List<string> _steps = new List<string>();

        public List<string> Steps
        {
            get { return _steps; }
        }

        protected override void ConfigureContainer()
        {
            Steps.Add("ConfigureContainer");
            base.ConfigureContainer();
        }

        protected override void CreateSystemUnderTest()
        {
            Steps.Add("CreateSystemUnderTest");
            base.CreateSystemUnderTest();
        }
    }
}