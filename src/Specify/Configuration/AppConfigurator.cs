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
        private readonly Type _testObject;
        public SpecifyConventions Conventions { get; set; }
        public IContainer Container { get; set; }

        public AppConfigurator(Type testObject)
        {
            _testObject = testObject;
        }

        public void Configure()
        {
            Container = CreateContainer(_testObject.Assembly);
            Conventions = GetConventions(_testObject.Assembly);
            ConfigureBddfy(Conventions);
        }

        public void ConfigureBddfy(SpecifyConventions config)
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

        public SpecifyConventions GetConventions(Assembly assembly)
        {
            var customConvention = GetTypesSafely(assembly)
                .FirstOrDefault(type => typeof(SpecifyConventions).IsAssignableFrom(type) && !type.IsInterface);
            return customConvention != null
                ? (SpecifyConventions)Activator.CreateInstance(customConvention)
                : new SpecifyConventions();
        }

        private static IEnumerable<Type> GetTypesSafely(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(x => x != null);
            }
        }

        public IContainer CreateContainer(Assembly assembly)
        {
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(AutoMockingContainer<>));
            builder.RegisterGeneric(typeof(IocContainer<>));
            builder
                .RegisterAssemblyTypes(assembly)
                .Where(t => typeof(ISpecification).IsAssignableFrom(t))
                .PropertiesAutowired();
            //builder.Register(c => GetDependencyResolver()).As<IDependencyLifetime>();
            var container = builder.Build();
            return container;
        }
    }
}