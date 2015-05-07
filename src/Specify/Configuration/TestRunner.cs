using System.Linq;

namespace Specify.Configuration
{
    internal class TestRunner
    {
        private readonly SpecifyBootstrapper _configuration;
        private readonly IApplicationContainer applicationContainer;
        private readonly ITestEngine _testEngine;

        public TestRunner(SpecifyBootstrapper configuration, IApplicationContainer applicationContainer,
            ITestEngine testEngine)
        {
            _configuration = configuration;
            this.applicationContainer = applicationContainer;
            _testEngine = testEngine;
        }

        public void Execute(IScenario testObject, string scenarioTitle = null)
        {
            foreach (var action in _configuration.PerTestActions)
            {
                action.Before();
            }

            using (var scenarioScope = this.applicationContainer.CreateChildContainer())
            {
                var scenario = (IScenario)scenarioScope.Resolve(testObject.GetType());
                var container = scenarioScope.Resolve<IScenarioContainer>();
                scenario.SetContainer(container);
                _testEngine.Execute(scenario);
            }

            foreach (var action in _configuration.PerTestActions.AsEnumerable().Reverse())
            {
                action.After();
            }
        }

        internal IApplicationContainer ApplicationContainer { get { return this.applicationContainer; } }
    }
}