using System.Linq;

namespace Specify.Configuration
{
    internal class TestRunner
    {
        private readonly SpecifyConfiguration _configuration;
        private readonly IDependencyResolver _dependencyResolver;
        private readonly ITestEngine _testEngine;

        public TestRunner(SpecifyConfiguration configuration, IDependencyResolver dependencyResolver,
            ITestEngine testEngine)
        {
            _configuration = configuration;
            _dependencyResolver = dependencyResolver;
            _testEngine = testEngine;
        }

        public void Execute(IScenario testObject, string scenarioTitle = null)
        {
            foreach (var action in _configuration.PerTestActions)
            {
                action.Before();
            }

            using (var scenarioScope = _dependencyResolver.CreateChildContainer())
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

        internal IDependencyResolver DependencyResolver { get { return _dependencyResolver; } }
    }
}