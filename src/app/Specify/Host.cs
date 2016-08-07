using System;
using Specify.Configuration;
using Specify.Configuration.Scanners;
using Specify.Logging;

namespace Specify
{
    internal static class Host
    {
        public static readonly IBootstrapSpecify Configuration;
        private static readonly ScenarioRunner _scenarioRunner;

        public static void Specify<TSut>(IScenario<TSut> testObject, string scenarioTitle = null) where TSut : class
        {
            _scenarioRunner.Execute(testObject, scenarioTitle);
        }

        static Host()
        {
#if NET40
            "Host".Log().DebugFormat("Registering AppDomain DomainUnload event");
            AppDomain.CurrentDomain.DomainUnload += (sender, e) => {
                _scenarioRunner.AfterAllScenarios();
            };
#else
            "Host".Log().DebugFormat("Registering System.Runtime.Loader.AssemblyLoadContext Unloading event");
            System.Runtime.Loader.AssemblyLoadContext.Default.Unloading += context => _scenarioRunner.AfterAllScenarios(); 
#endif
            var scanner = ConfigScannerFactory.SelectScanner();
            Configuration = scanner.GetConfiguration();

            _scenarioRunner = new ScenarioRunner(Configuration, new BddfyTestEngine());
            _scenarioRunner.BeforeAllScenarios();
        }
    }
}