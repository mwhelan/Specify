using System;
using Specify.Configuration;
using Specify.Configuration.Scanners;
using Specify.Logging;

namespace Specify
{
    public static class Host
    {
        public static readonly IBootstrapSpecify Configuration;
        private static readonly ScenarioRunner _scenarioRunner;

        internal static void Specify<TSut>(IScenario<TSut> testObject, string scenarioTitle = null) where TSut : class
        {
            _scenarioRunner.Execute(testObject, scenarioTitle);
        }

        static Host()
        {
            "Host".Log().DebugFormat("Registering System.Runtime.Loader.AssemblyLoadContext Unloading event");
            System.Runtime.Loader.AssemblyLoadContext.Default.Unloading += context => _scenarioRunner.AfterAllScenarios(); 

            var scanner = ConfigScannerFactory.SelectScanner();
            Configuration = scanner.GetConfiguration();

            _scenarioRunner = new ScenarioRunner(Configuration, new BddfyTestEngine());
            _scenarioRunner.BeforeAllScenarios();
        }
    }
}