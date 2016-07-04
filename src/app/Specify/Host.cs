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
#if NET40
            AppDomain.CurrentDomain.DomainUnload += (sender, e) => {
                _scenarioRunner.AfterAllScenarios();
            };
#else
            System.Runtime.Loader.AssemblyLoadContext.Default.Unloading += context => _scenarioRunner.AfterAllScenarios(); 
#endif

            Configuration = ConfigScannerFactory
                .SelectScanner()
                .GetConfiguration();

            _scenarioRunner = new ScenarioRunner(Configuration, new BddfyTestEngine());
            _scenarioRunner.BeforeAllScenarios();
        }
    }
}