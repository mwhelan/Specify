using System.Linq;

namespace Specify.Configuration
{
    internal class TestRunner
    {
        private readonly SpecifyBootstrapper _configuration;
        private readonly IApplicationContainer _applicationContainer;
        private readonly ITestEngine _testEngine;

        public TestRunner(SpecifyBootstrapper configuration, IApplicationContainer applicationContainer,
            ITestEngine testEngine)
        {
            _configuration = configuration;
            _applicationContainer = applicationContainer;
            _testEngine = testEngine;
        }

        public void Execute(IScenario testObject, string scenarioTitle = null)
        {
            using (var scenarioScope = _applicationContainer.CreateChildContainer())
            {
                var container = scenarioScope.Resolve<IScenarioContainer>();

                foreach (var action in _configuration.PerTestActions)
                {
                    action.Before(container);
                }

                var scenario = (IScenario)scenarioScope.Resolve(testObject.GetType());
                scenario.SetContainer(container);
                _testEngine.Execute(scenario);

                foreach (var action in _configuration.PerTestActions.AsEnumerable().Reverse())
                {
                    action.After();
                }
            }
        }

        internal IApplicationContainer ApplicationContainer { get { return _applicationContainer; } }
    }
}