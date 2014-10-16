using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Specify.Containers;
using TestStack.BDDfy.Configuration;
using TestStack.BDDfy.Reporters.Html;

namespace Specify.Configuration
{
    internal class AppConfigurator
    {
        public SpecifyConfiguration Configure()
        {
            var customConvention = AssemblyTypeResolver.GetAllTypesFromAppDomain()
                .FirstOrDefault(type => typeof(SpecifyConfiguration).IsAssignableFrom(type) && type.IsClass);
            return customConvention != null
                ? (SpecifyConfiguration)Activator.CreateInstance(customConvention)
                : new SpecifyConfiguration();
        }

        public void ConfigureBddfy(SpecifyConfiguration config)
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

            Configurator.Scanners.StoryMetadataScanner = () => new SpecifyStoryMetadataScanner();
        }
    }
}