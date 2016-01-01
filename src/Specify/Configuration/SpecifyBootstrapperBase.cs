using System;
using System.Collections.Generic;
using Specify.Mocks;
using TestStack.BDDfy.Configuration;
using TestStack.BDDfy.Reporters.Html;

namespace Specify.Configuration
{
    /// <summary>
    /// The base configuration class providing common functionality to bootstrap Specify.
    /// </summary>
    public abstract class SpecifyBootstrapperBase : IConfigureSpecify
    {
        /// <summary>
        /// Builds the application container.
        /// </summary>
        /// <returns>IContainer.</returns>
        protected abstract IContainer BuildApplicationContainer();

        /// <inheritdoc />
        public IContainer ApplicationContainer { get; internal set; }

        /// <inheritdoc />
        public List<IPerAppDomainActions> PerAppDomainActions { get; } = new List<IPerAppDomainActions>();

        /// <inheritdoc />
        public List<IPerScenarioActions> PerScenarioActions { get; } = new List<IPerScenarioActions>();

        /// <inheritdoc />
        public bool LoggingEnabled { get; set; } = false;

        /// <inheritdoc />
        public HtmlReportConfiguration HtmlReport { get; } = new HtmlReportConfiguration();

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecifyBootstrapperBase"/> class.
        /// </summary>
        public void Configure()
        {
            ConfigureReport();
            ApplicationContainer = BuildApplicationContainer();
        }

        /// <summary>
        /// Override default behaviour. By default, Specify will detect NSubstitute, FakeItEasy and Moq, in that order.
        /// If none are found, or null is returned as the mock factory, then the full TinyIoc container will be used without mocking. 
        /// </summary>
        /// <returns>Func&lt;IMockFactory&gt;.</returns>
        public virtual Func<IMockFactory> GetMockFactory()
        {
            return new MockDetector().FindAvailableMock();
        }

        /// <summary>
        /// Configures the BDDfy HTML report using values specified in the 'HtmlReport' property.
        /// </summary>
        public virtual void ConfigureReport()
        {
            Configurator.BatchProcessors.HtmlReport.Disable();
            if (HtmlReport.ReportType == HtmlReportConfiguration.HtmlReportType.Classic)
            {
                Configurator.BatchProcessors.Add(new HtmlReporter(HtmlReport));
            }
            else
            {
                Configurator.BatchProcessors.Add(new HtmlReporter(HtmlReport, new MetroReportBuilder()));
            }
        }
    }
}