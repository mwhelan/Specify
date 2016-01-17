using System;
using Specify.Configuration;
using Specify.Configuration.Scanners;

namespace Specify
{
    internal static class Host
    {
        public static readonly IBootstrapSpecify Configuration;
        private static readonly ScenarioRunner _scenarioRunner;

        public static void Specify(IScenario testObject, string scenarioTitle = null)
        {
            _scenarioRunner.Execute(testObject, scenarioTitle);
        }

        static Host()
        {
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;

            Configuration = ConfigScannerFactory
                .SelectScanner()
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