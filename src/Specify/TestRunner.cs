using System;
using System.Linq;
using Specify.Configuration;
using TestStack.BDDfy;
using TestStack.BDDfy.Configuration;
using TestStack.BDDfy.Reporters.Html;

namespace Specify
{
    internal static class TestRunner
    {
        private static readonly SpecifyConfiguration _configuration;

        public static void Specify(object testObject, string scenarioTitle = null)
        {
            foreach (var action in _configuration.PerTestActions)
            {
                action.Before();
            }
            testObject.BDDfy(scenarioTitle);
            foreach (var action in _configuration.PerTestActions.AsEnumerable().Reverse())
            {
                action.After();
            }
        }

        static TestRunner()
        {
            _configuration = Configure();
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
            _configuration.PerAppDomainActions.ForEach(action => action.Before());
        }

        static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            foreach (var action in _configuration.PerAppDomainActions.AsEnumerable().Reverse())
            {
                action.After();
            }
        }

        static SpecifyConfiguration Configure()
        {
            var customConvention = AssemblyTypeResolver.GetAllTypesFromAppDomain()
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

            // Chill
            Configurator.Scanners.DefaultMethodNameStepScanner.Disable();
            Configurator.Scanners.Add(() => new ChillMethodNameStepScanner());

            return config;
        }

    }
}