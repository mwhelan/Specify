using TestStack.BDDfy;

namespace Specify.Tests.Stubs
{
    internal class UnitScenarioWithAllSupportedStepsInRandomOrderWithExamples 
        : TestableUnitScenario<ConcreteObjectWithNoConstructor>
    {
        public UnitScenarioWithAllSupportedStepsInRandomOrderWithExamples()
        {
            Steps.Add("Constructor");
            Examples = new ExampleTable("Start", "Eat", "Left")
            {
                {12, 5, 7},
                {20, 5, 15}
            };
        }
        
        public override void BeginTestCase()
        {
            Steps.Add("BeginTestCase");
        }

        public void TearDown()
        {
            Steps.Add("TearDown");
        }

        public override void Setup()
        {
            Steps.Add("Setup");
        }

        public void AndGivenSomeOtherPrecondition(int start, int eat, int left)
        {
            Steps.Add("AndGivenSomeOtherPrecondition");
        }

        public override void EndTestCase()
        {
            Steps.Add("EndTestCase");
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