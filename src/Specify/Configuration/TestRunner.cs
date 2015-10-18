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
            using (var container = _applicationContainer.Resolve<IScenarioContainer>())
            {
                //var container = scenarioScope.Resolve<IScenarioContainer>();

                foreach (var action in _configuration.PerScenarioActions)
                {
                    action.Before(container);
                }

                var scenario = (IScenario)container.Resolve(testObject.GetType());
                scenario.SetContainer(container);
                _testEngine.Execute(scenario);

                foreach (var action in _configuration.PerScenarioActions.AsEnumerable().Reverse())
                {
                    action.After();
                }
            }
        }

        internal IApplicationContainer ApplicationContainer { get { return _applicationContainer; } }
        internal SpecifyBootstrapper Configuration { get { return _configuration; } }
    }
}