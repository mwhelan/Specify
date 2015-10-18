using System.Linq;

namespace Specify.Configuration
{
    internal class TestRunner
    {
        private readonly ITestEngine _testEngine;

        public TestRunner(SpecifyBootstrapper configuration, IApplicationContainer applicationContainer,
            ITestEngine testEngine)
        {
            Configuration = configuration;
            ApplicationContainer = applicationContainer;
            _testEngine = testEngine;
        }

        public void Execute(IScenario testObject, string scenarioTitle = null)
        {
            using (var container = ApplicationContainer.CreateChildContainer())
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

        internal IApplicationContainer ApplicationContainer { get; set; }

        internal SpecifyBootstrapper Configuration { get; set; }
    }
}