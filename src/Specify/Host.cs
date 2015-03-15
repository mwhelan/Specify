using System;
using System.Linq;
using Specify.Configuration;
using Specify.Containers;
using TestStack.BDDfy;
using TestStack.BDDfy.Configuration;

namespace Specify
{
    internal static class Host
    {
        private static readonly SpecifyConfiguration _configuration;
        private static readonly IContainer _container;

        public static void Specify(object testObject, string scenarioTitle = null)
        {
            foreach (var action in _configuration.PerTestActions)
            {
                action.Before();
            }

            using (var lifetimeScope = _container.CreateChildContainer())
            {
                var specification = (dynamic) _container.Resolve(testObject.GetType());
                specification.Container = new SutFactory(lifetimeScope);
                specification.BDDfy(scenarioTitle);
            }
            
            foreach (var action in _configuration.PerTestActions.AsEnumerable().Reverse())
            {
                action.After();
            }
        }

        static Host()
        {
            _configuration = Configure();
            _container = _configuration.GetSpecifyContainer();
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

            return config;
        }
    }
}