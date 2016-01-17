using System.Linq;
using System.Runtime.CompilerServices;
using Specify.Logging;
using TestStack.BDDfy.Configuration;

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

        public void Execute(IScenario testObject, string scenarioTitle = null)
        {
            using (var container = Configuration.ApplicationContainer.Get<IContainer>())
            {
                foreach (var action in Configuration.PerScenarioActions)
                {
                    action.Before(container);
                }

                var scenario = (IScenario)container.Get(testObject.GetType());
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