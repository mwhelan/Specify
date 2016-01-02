using System;
using System.Linq;
using Specify.Configuration;
using Specify.lib;
using Specify.Logging;
using TestStack.BDDfy.Configuration;

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
            Configuration = new ConfigurationScanner().GetConfiguration();

            _scenarioRunner = new ScenarioRunner(Configuration, new BddfyTestEngine());
            _scenarioRunner.BeforeAllScenarios();

            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
        }

        static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            _scenarioRunner.AfterAllScenarios();
        }
    }
}