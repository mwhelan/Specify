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

        protected override void CreateSut()
        {
            Steps.Add("SPECIFICATION - EstablishContext");
            base.CreateSut();
        }
    }
}