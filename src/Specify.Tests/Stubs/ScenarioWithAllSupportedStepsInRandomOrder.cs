namespace Specify.Tests.Stubs
{
    internal class ScenarioWithAllSupportedStepsInRandomOrder : TestableScenarioFor<ConcreteObjectWithNoConstructor>
    {
        public ScenarioWithAllSupportedStepsInRandomOrder()
        {
            Steps.Add("Implementation - Constructor");
        }

        public void TearDown()
        {
            Steps.Add("Implementation - TearDown");
        }
        public void Setup()
        {
            Steps.Add("Implementation - Setup");
        }

        public void AndGivenSomeOtherPrecondition()
        {
            Steps.Add("Implementation - AndGivenSomeOtherPrecondition");
        }

        public void ThenAnExpectation()
        {
            Steps.Add("Implementation - ThenAnExpectation");
        }

        public void AndThenAnotherExpectation()
        {
            Steps.Add("Implementation - AndThenAnotherExpectation");
        }
        public void WhenAction()
        {
            Steps.Add("Implementation - WhenAction");
        }

        public void GivenSomePrecondition()
        {
            Steps.Add("Implementation - GivenSomePrecondition");
        }

        public void AndWhenAnotherAction()
        {
            Steps.Add("Implementation - AndWhenAnotherAction");
        }
    }
}