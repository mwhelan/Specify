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
        public SpecifyConfiguration Configuration { get; set; }
        public IContainer Container { get; set; }

        public AppConfigurator(ISpecification testObject)
        {
            _testObject = testObject.GetType();
        }

        public void Configure()
        {
            Container = CreateContainer(_testObject.Assembly);
            Configuration = GetConventions(_testObject.Assembly);
            ConfigureBddfy(Configuration);
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

        public SpecifyConfiguration GetConventions(Assembly assembly)
        {
            var customConvention = GetTypesSafely(assembly)
                .FirstOrDefault(type => typeof(SpecifyConfiguration).IsAssignableFrom(type) && !type.IsInterface);
            return customConvention != null
                ? (SpecifyConfiguration)Activator.CreateInstance(customConvention)
                : new SpecifyConfiguration();
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
            //builder.RegisterGeneric(typeof(AutoMockingContainer<>));
            //builder.RegisterGeneric(typeof(IocContainer<>));
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