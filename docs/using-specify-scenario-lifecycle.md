# Scenario Lifecycle
Specify uses BDDfy's Reflective API to scan Scenario classes for methods. By default, BDDfy recognises the standard BDD methods, as well as Setup and TearDown. You can read more about them [here](http://www.mehdi-khalili.com/bddify-in-action/method-name-conventions) and you can always [customize them](http://www.michael-whelan.net/roll-your-own-testing-framework/) if you have your own preferences by creating a new BDDfy `MethodNameStepScanner`. 

The methods are found based on these method name conventions - and run in this order. 

* Ending with `Context` is considered as a setup method. `Not reported`.
* `Setup` is considered as a setup method. `Not reported`.
* Starting with `Given` is considered as a setup method.
* Starting with `AndGiven` is considered as a setup method that runs after Given steps.
* Starting with `When` is considered as a transition method.
* Starting with `AndWhen` is considered as a transition method that runs after When steps.
* Starting with `Then` is considered as an asserting method.
* Starting with `And` is considered as an asserting method.
* Starting with `TearDown` is considered as a finally method which is run after all the other steps. `Not reported`.

Each of the above methods is displayed on reports unless otherwise stated.

This example shows a scenario class with all of the method names that Specify supports out of the box in a random order. 

    internal class UserStoryScenarioWithAllSupportedStepsInRandomOrder 
		: ScenarioFor<ConcreteObjectWithNoConstructor>
    {
        public UserStoryScenarioWithAllSupportedStepsInRandomOrder()
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

All of the methods are executed and the `Setup` and `Teardown` methods are not displayed on the report:

	Scenario: User Story Scenario With All Supported Steps In Random Order
		Given some precondition
		  And some other precondition
		When action
		  And another action
		Then an expectation
		  And another expectation