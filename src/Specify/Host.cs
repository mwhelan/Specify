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
        private static readonly TestRunner _testRunner;

        public static void Specify(IScenario testObject, string scenarioTitle = null)
        {
            _testRunner.Execute(testObject, scenarioTitle);
        }

        static Host()
        {
            Configuration = Configure();
       
            _testRunner = new TestRunner(Configuration, new BddfyTestEngine());
            _testRunner.LogSpecifyConfiguration();
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
            Configuration.PerAppDomainActions.ForEach(action => action.Before());
        }

        static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            "Host".Log().DebugFormat("Specify - DomainUnload");
            foreach (var action in Configuration.PerAppDomainActions.AsEnumerable().Reverse())
            {
                "Host".Log().DebugFormat("{0} After", action.GetType().Name);
                action.After();
            }
            Configuration.ApplicationContainer.Dispose();
        }

        static IConfigureSpecify Configure()
        {
            var customConvention = AssemblyTypeResolver
                .GetAllTypesFromAppDomain()
                .FirstOrDefault(type => typeof(IConfigureSpecify).IsAssignableFrom(type) && type.IsClass);
            var config = customConvention != null
                ? (IConfigureSpecify)Activator.CreateInstance(customConvention)
                : new SpecifyBootstrapper();

            Configurator.Scanners.StoryMetadataScanner = () => new SpecifyStoryMetadataScanner();
            "Host".Log().Debug("Added SpecifyStoryMetadataScanner to BDDfy pipeline.");
            
            if (config.LoggingEnabled)
            {
                Configurator.Processors.Add(() => new ScenarioLoggingProcessor());
            }

            return config;
        }
    }
}