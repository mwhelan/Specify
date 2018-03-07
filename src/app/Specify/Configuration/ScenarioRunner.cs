using System.Linq;
using Specify.Configuration.Examples;
using Specify.Logging;

namespace Specify.Configuration
{
    internal class ScenarioRunner
    {
        private readonly ITestEngine _testEngine;

        public ScenarioRunner(IBootstrapSpecify configuration, ITestEngine testEngine)
        {
            Configuration = configuration;
            _testEngine = testEngine;
        }

        public void Execute<TSut>(IScenario<TSut> testObject, string scenarioTitle = null) where TSut : class
        {
            var scenario = (IScenario<TSut>) Configuration.ApplicationContainer.Get(testObject.GetType());
            scenario.SetContainer(Configuration.ApplicationContainer);
            _testEngine.Execute(scenario);
        }

        public void BeforeAllScenarios()
        {
            this.Log().DebugFormat("BeforeAllScenarios");

            Configuration.InitializeSpecify();

            foreach (var action in Configuration.PerAppDomainActions)
            {
                this.Log().DebugFormat("Executing {0} PerAppDomain Before action", action.GetType().Name);
                action.Before();
            }
        }

        public void AfterAllScenarios()
        {
            this.Log().DebugFormat("AfterAllScenarios");

            foreach (var action in Configuration.PerAppDomainActions.AsEnumerable().Reverse())
            {
                this.Log().DebugFormat("Executing {0} PerAppDomain After action", action.GetType().Name);
                action.After();
            }

            Configuration.ApplicationContainer.Dispose();
        }

        internal IBootstrapSpecify Configuration { get; set; }
    }
}