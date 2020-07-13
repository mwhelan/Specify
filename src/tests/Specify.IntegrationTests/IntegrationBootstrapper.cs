using System;
using DryIoc;
using Specify.Configuration;
using Specify.Configuration.Scanners;
using Specify.Mocks;
using TestStack.BDDfy.Configuration;

namespace Specify.IntegrationTests
{
    /// <summary>
    /// The startup class to configure Specify with the default TinyIoc container. 
    /// Make any changes to the default configuration settings in this file.
    /// </summary>
    public class IntegrationBootstrapper : DefaultBootstrapper
    {
        public IntegrationBootstrapper()
        {
            HtmlReport.ReportHeader = "API Template";
            HtmlReport.ReportDescription = "Unit Specs";
            HtmlReport.OutputFileName = "ApiTemplate-UnitSpecs.html";
            Configurator.BatchProcessors.HtmlReport.Disable();
        }

        /// <summary>
        /// Register any additional items into the DryIoc container or leave it as it is. 
        /// </summary>
        /// <param name="container">The <see cref="DryIoc"/> container.</param>
        public override void ConfigureContainer(Container container)
        {
            // container.Register<IPerScenarioAction, LoggingAction>();
        }
    }

    public class IntegrationConfigScanner : ConfigScanner
    {
        /// <inheritdoc />
        protected override Type DefaultBootstrapperType => typeof(IntegrationBootstrapper);

        public IntegrationConfigScanner(IFileSystem fileSystem)
            : base(fileSystem) { }

        public IntegrationConfigScanner()
            : this(new FileSystem()) { }
    }
}