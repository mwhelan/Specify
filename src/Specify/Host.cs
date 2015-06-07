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
        private static readonly SpecifyBootstrapper _configuration;
        private static readonly IApplicationContainer applicationContainer;
        private static readonly TestRunner _testRunner;
        private static ILog _logger = LogProvider.GetLogger("Specify.Host");

        public static void Specify(IScenario testObject, string scenarioTitle = null)
        {
            _testRunner.Execute(testObject, scenarioTitle);
        }

        static Host()
        {
            _configuration = Configure();
            applicationContainer = _configuration.CreateApplicationContainer();
            _logger.Log("Type of ApplicationContainer is {0}", applicationContainer.GetType().Name);
            
            _testRunner = new TestRunner(_configuration, applicationContainer,new BddfyTestEngine());
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
            _configuration.PerAppDomainActions.ForEach(action => action.Before(applicationContainer));
        }

        static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            _logger.Log("Specify - DomainUnload");
            foreach (var action in _configuration.PerAppDomainActions.AsEnumerable().Reverse())
            {
                _logger.Log("{0} After", action.GetType().Name);
                action.After();
            }
            applicationContainer.Dispose();
        }

        static SpecifyBootstrapper Configure()
        {
            _logger.Log("Looking for custom bootstrapper.");
            var customConvention = AssemblyTypeResolver
                .GetAllTypesFromAppDomain()
                .FirstOrDefault(type => typeof(SpecifyBootstrapper).IsAssignableFrom(type) && type.IsClass);
            var config = customConvention != null
                ? (SpecifyBootstrapper)Activator.CreateInstance(customConvention)
                : new SpecifyBootstrapper();
            _logger.Log("Using {0} bootstrapper.", config.GetType().Name);

            _logger.Log("Adding SpecifyStoryMetadataScanner to BDDfy pipeline.");
            Configurator.Scanners.StoryMetadataScanner = () => new SpecifyStoryMetadataScanner();
            
            if (config.LoggingEnabled)
            {
                _logger.Log("Enabling logging as per bootstrapper.");
                Configurator.Processors.Add(() => new LoggingProcessor());
            }

            return config;
        }
    }
}