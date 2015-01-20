using System;
using System.Linq;
using Specify.Configuration;

using TestStack.BDDfy;
using TestStack.BDDfy.Configuration;
using TestStack.BDDfy.Reporters.Html;

namespace Specify
{
    internal static class Host
    {
        private static readonly SpecifyConfiguration _configuration;
        private static readonly ISpecifyContainer _container;

        public static void Specify(object testObject, string scenarioTitle = null)
        {
            foreach (var action in _configuration.PerTestActions)
            {
                action.Before();
            }

            using (var lifetimeScope = _container.CreateTestLifetimeScope())
            {
                var specification = _container.Resolve(testObject.GetType());
                specification.Container = lifetimeScope;
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

            if (config.HtmlReport != null)
            {
                var reportConfiguration = new DefaultHtmlReportConfiguration();
                reportConfiguration.ReportHeader = config.HtmlReport.Header;
                reportConfiguration.ReportDescription = config.HtmlReport.Description;
                reportConfiguration.OutputFileName = config.HtmlReport.Name;

                Configurator.BatchProcessors.HtmlReport.Disable();
                if (config.HtmlReport.Type == HtmlReportConfiguration.ReportType.Html)
                {
                    Configurator.BatchProcessors.Add(new HtmlReporter(reportConfiguration));
                }
                else
                {
                    Configurator.BatchProcessors.Add(new HtmlReporter(reportConfiguration, new MetroReportBuilder()));

                }
            }

            Configurator.Scanners.StoryMetadataScanner = () => new SpecifyStoryMetadataScanner();

            return config;
        }

    }
}