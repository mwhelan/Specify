using System;
using Autofac;
using Specify.Containers;
using Specify.Core;
using Specify.Scanners;
using TestStack.BDDfy.Configuration;
using TestStack.BDDfy.Reporters.Html;

namespace Specify.Configuration
{
    public static class SpecifyConfigurator
    {
        private static ISpecifyConfig _configuration;
        public static IContainer Container { get; set; }

        public static void Initialize(object testObject)
        {
            _configuration = new StartupScanner().GetConfig(testObject.GetType());
            ConfigureBddfy(_configuration, testObject.GetType());
            Container = CreateContainer(testObject.GetType());

            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
            _configuration.BeforeAllTests();
        }

        static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            _configuration.AfterAllTests();
        }

        public static IDependencyLifetime GetDependencyResolver()
        {
            return _configuration.GetChildContainer().Invoke();
        }

        private static IContainer CreateContainer(Type testObjectType)
        {
            var assembly = testObjectType.Assembly;
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(assembly).Where(t => typeof(ISpecification).IsAssignableFrom(t)).PropertiesAutowired();
            builder.Register(c => GetDependencyResolver()).As<IDependencyLifetime>();
            var container = builder.Build();
            return container;
        }

        private static void ConfigureBddfy(ISpecifyConfig config, Type testObjectType)
        {
            var reportConfiguration = new DefaultHtmlReportConfiguration();
            reportConfiguration.ReportHeader = config.Report.Header;
            reportConfiguration.ReportDescription = config.Report.Description;
            reportConfiguration.OutputFileName = config.Report.Name;

            Configurator.BatchProcessors.HtmlReport.Disable();
            if (config.Report.Type == ReportConfig.ReportType.Html)
            {
                Configurator.BatchProcessors.Add(new HtmlReporter(reportConfiguration));
            }
            else
            {
                Configurator.BatchProcessors.Add(new HtmlReporter(reportConfiguration, new MetroReportBuilder()));
            }

            if (typeof(ScenarioFor<,>).IsAssignableFrom(testObjectType))
            {
                Configurator.Scanners.StoryMetadataScanner = () => new SpecStoryMetaDataScanner();
            }
            else
            {
                Configurator.Scanners.StoryMetadataScanner = () => new ScenarioStoryMetaDataScanner();
            }
        }
    }
}