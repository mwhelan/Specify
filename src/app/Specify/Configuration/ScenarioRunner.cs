using System.Collections.Generic;
using System.Linq;
using Specify.Configuration.Examples;
using Specify.Logging;

namespace Specify.Configuration
{
    internal class ScenarioRunner
    {
        private readonly ITestEngine _testEngine;
        private IEnumerable<IPerApplicationAction> _actions;

        public ScenarioRunner(IBootstrapSpecify configuration, ITestEngine testEngine)
        {
            Configuration = configuration;
            _testEngine = testEngine;
        }

        public void Execute<TSut>(IScenario<TSut> testObject, string scenarioTitle = null) where TSut : class
        {
            var scenario = (IScenario<TSut>) Configuration.ApplicationContainer.Get(testObject.GetType());
            var exampleScope = Configuration.ApplicationContainer.Get<ITestScope>();
            scenario.SetExampleScope(exampleScope);
            _testEngine.Execute(scenario);
        }

        public void BeforeAllScenarios()
        {
            this.Log().DebugFormat("BeforeAllScenarios");

            Configuration.InitializeSpecify();

            _actions = Configuration.ApplicationContainer.GetMultiple<IPerApplicationAction>();
            foreach (var action in _actions.OrderBy(x => x.Order))
            {
                this.Log().DebugFormat("Executing {0} PerApplication Before action", action.GetType().Name);
                action.Before();
            }
        }

        public void AfterAllScenarios()
        {
            this.Log().DebugFormat("AfterAllScenarios");

            foreach (var action in _actions.OrderByDescending(x => x.Order))
            {
                this.Log().DebugFormat("Executing {0} PerApplication After action", action.GetType().Name);
                action.After();
            }

            Configuration.ApplicationContainer.Dispose();
        }

        internal IBootstrapSpecify Configuration { get; set; }
    }
}