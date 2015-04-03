using System;
using System.Linq;
using Specify.Configuration;
using Specify.Containers;
using Specify.lib;
using TestStack.BDDfy.Configuration;

namespace Specify
{
    internal static class Host
    {
        private static readonly SpecifyConfiguration _configuration;
        private static readonly IContainer _container;
        private static readonly TestRunner _testRunner;

        public static void Specify(IScenario testObject, string scenarioTitle = null)
        {
            _testRunner.Execute(testObject, scenarioTitle);
        }

        static Host()
        {
            _configuration = Configure();
            _container = _configuration.GetSpecifyContainer();
            _testRunner = new TestRunner(_configuration, _container,new BddfyTestEngine());
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
            _configuration.PerAppDomainActions.ForEach(action => action.Before());
        }

        static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            foreach (var action in _configuration.PerAppDomainActions.AsEnumerable().Reverse())
            {
                action.After();
            }
            _container.Dispose();
        }

        static SpecifyConfiguration Configure()
        {
            var customConvention = AssemblyTypeResolver
                .GetAllTypesFromAppDomain()
                .FirstOrDefault(type => typeof(SpecifyConfiguration).IsAssignableFrom(type) && type.IsClass);
            var config = customConvention != null
                ? (SpecifyConfiguration)Activator.CreateInstance(customConvention)
                : new SpecifyConfiguration();

            Configurator.Scanners.StoryMetadataScanner = () => new SpecifyStoryMetadataScanner();
            if (config.LoggingEnabled)
            {
                Configurator.Processors.Add(() => new LoggingProcessor());
            }

            return config;
        }
    }
}