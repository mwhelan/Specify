using System.Linq;
using Specify.Logging;
using TestStack.BDDfy.Processors;
using TestStack.BDDfy.Reporters.Html;

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

        public void Execute<TSut>(IScenario<TSut> scenario, string scenarioTitle = null) where TSut : class
        {
            using (var container = Configuration.ApplicationContainer.Get<IContainer>())
            {
                scenario.SetContainer(container);

                foreach (var action in Configuration.PerScenarioActions)
                {
                    if (action.ShouldExecute(scenario.GetType()))
                    {
                        action.Before(scenario);
                    }
                }

                _testEngine.Execute(scenario);

                foreach (var action in Configuration.PerScenarioActions.AsEnumerable().Reverse())
                {
                    if (action.ShouldExecute(scenario.GetType()))
                    {
                        action.After();
                    }
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

            var reporter = new HtmlReporter(new ClassicReportBuilder());
            reporter.Process(StoryCache.Stories);


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