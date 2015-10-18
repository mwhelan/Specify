using Specify.Configuration;
using Specify.Logging;

namespace Specify
{
    internal static class LoggingExtensions
    {
        internal static void LogSpecifyConfiguration(this TestRunner testRunner)
        {
            string containerName;
            using (IScenarioContainer container = testRunner.ApplicationContainer.CreateChildContainer())
            {
                containerName = container.GetType().FullName;
            }

            var log = "TestRunner".Log();
            log.DebugFormat("Bootstrapper: {Bootstrapper}", testRunner.Configuration.GetType().FullName);
            log.DebugFormat("ApplicationContainer: {ApplicationContainer}", testRunner.ApplicationContainer.GetType().FullName);
            log.DebugFormat("ScenarioContainer: {ScenarioContainer}", containerName);
            log.DebugFormat("PerAppDomainActions: {PerAppDomainActionCount}", testRunner.Configuration.PerAppDomainActions.Count);
            foreach (var action in testRunner.Configuration.PerAppDomainActions)
            {
                log.DebugFormat("- Action: {PerAppDomainAction}", action.GetType().Name);
            }
            log.DebugFormat("PerScenarioActions: {PerScenarioActionCount}", testRunner.Configuration.PerScenarioActions.Count);
            foreach (var action in testRunner.Configuration.PerScenarioActions)
            {
                log.DebugFormat("- Action: {PerScenarioAction}", action.GetType().Name);
            }
            log.DebugFormat("Logging Enabled = {LoggingEnabled}.", testRunner.Configuration.LoggingEnabled);
        }
    }
}