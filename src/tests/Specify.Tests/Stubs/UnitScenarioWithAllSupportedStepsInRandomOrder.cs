using Specify.Tests.SpecifyRootFolder;

namespace Specify.Tests.Stubs
{
    internal class UnitScenarioWithAllSupportedStepsInRandomOrder 
        : TestableUnitScenario<ConcreteObjectWithNoConstructor>
    {
        public UnitScenarioWithAllSupportedStepsInRandomOrder()
        {
            Steps.Add("Constructor");
        }
        
        public void TearDown()
        {
            Steps.Add("TearDown");
        }

        public void Setup()
        {
            Steps.Add("Setup");
        }

        public void AndGivenSomeOtherPrecondition()
        {
            Steps.Add("AndGivenSomeOtherPrecondition");
        }

        public new void BeginTestCase()
        {
            Steps.Add("BeginTestCase");
            base.BeginTestCase();
        }

        public new void EndTestCase()
        {
            Steps.Add("EndTestCase");
            base.EndTestCase();
        }

        public void ThenAnExpectation()
        {
            Steps.Add("ThenAnExpectation");
        }

        public void EstabablishContext()
        {
            Steps.Add("EstablishContext");
        }

        public void AndThenAnotherExpectation()
        {
            Steps.Add("AndThenAnotherExpectation");
        }
        public void WhenAction()
        {
            Steps.Add("WhenAction");
        }

        public void GivenSomePrecondition()
        {
            Steps.Add("GivenSomePrecondition");
        }

        public void AndWhenAnotherAction()
        {
            Steps.Add("AndWhenAnotherAction");
        }
    }
}