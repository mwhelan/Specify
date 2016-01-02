using System;
using Specify.Configuration;

namespace Specify
{
    internal static class Host
    {
        public static readonly IConfigureSpecify Configuration;
        private static readonly ScenarioRunner _scenarioRunner;

        public static void Specify(IScenario testObject, string scenarioTitle = null)
        {
            _scenarioRunner.Execute(testObject, scenarioTitle);
        }

        static Host()
        {
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;

            Configuration = ConfigurationScanner
                .FindScanner()
                .GetConfiguration();

            _scenarioRunner = new ScenarioRunner(Configuration, new BddfyTestEngine());
            _scenarioRunner.BeforeAllScenarios();
        }

        static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            _scenarioRunner.AfterAllScenarios();
        }
    }
}