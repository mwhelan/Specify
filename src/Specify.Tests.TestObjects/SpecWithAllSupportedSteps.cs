using System.Collections.Generic;
using Specify.Containers;
using Specify.Core;

namespace Specify.Tests.TestObjects
{
    public class SpecWithAllSupportedSteps : TestableSpecificationFor<object>
    {
        public void GivenSomePrecondition()
        {
            Steps.Add("SpecWithAllSupportedSteps.GivenSomePrecondition");
        }

        public void AndGivenSomeOtherPrecondition()
        {
            Steps.Add("SpecWithAllSupportedSteps.AndGivenSomeOtherPrecondition");
        }

        public void WhenAction()
        {
            Steps.Add("SpecWithAllSupportedSteps.WhenAction");
        }

        public void AndWhenAnotherAction()
        {
            Steps.Add("SpecWithAllSupportedSteps.AndWhenAnotherAction");
        }

        public void ThenAnExpectation()
        {
            Steps.Add("SpecWithAllSupportedSteps.ThenAnExpectation");
        }

        public void AndThenAnotherExpectation()
        {
            Steps.Add("SpecWithAllSupportedSteps.AndThenAnotherExpectation");
        }

        //public override void EstablishContext()
        //{
        //    Steps.Add("SpecWithAllSupportedSteps.EstablishContext");
        //}

        public void Setup()
        {
            Steps.Add("SpecWithAllSupportedSteps.Setup");
        }

        //public override void TearDown()
        //{
        //    Steps.Add("SpecWithAllSupportedSteps.TearDown");
        //}
    }
}