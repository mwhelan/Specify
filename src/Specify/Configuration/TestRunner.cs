using System.Linq;

namespace Specify.Configuration
{
    internal class TestRunner
    {
        private readonly ITestEngine _testEngine;

        public TestRunner(IConfigureSpecify configuration, ITestEngine testEngine)
        {
            Configuration = configuration;
            _testEngine = testEngine;
        }

        public void Execute(IScenario testObject, string scenarioTitle = null)
        {
            using (var container = Configuration.ApplicationContainer.Resolve<IContainer>())
            {
                foreach (var action in Configuration.PerScenarioActions)
                {
                    action.Before(container);
                }

                var scenario = (IScenario)container.Resolve(testObject.GetType());
                scenario.SetContainer(container);
                _testEngine.Execute(scenario);

                foreach (var action in Configuration.PerScenarioActions.AsEnumerable().Reverse())
                {
                    action.After();
                }
            }
        }

        public void BeforeAllScenarios()
        {
            
        }

        public void AfterAllScenarios()
        {
            
        }

        internal IConfigureSpecify Configuration { get; set; }
    }
}