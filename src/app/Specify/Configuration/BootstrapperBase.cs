using System.Collections.Generic;
using Specify.Logging;
using Specify.Mocks;
using TestStack.BDDfy.Configuration;
using TestStack.BDDfy.Reporters.Html;

namespace Specify.Configuration
{
    /// <summary>
    /// The base bootstrapper class providing common functionality to configure Specify at startup.
    /// </summary>
    public abstract class BootstrapperBase : IBootstrapSpecify
    {
        private IMockFactory _mockFactory;

        /// <summary>
        /// Builds the application container.
        /// </summary>
        /// <returns>IContainer.</returns>
        protected abstract IContainer BuildApplicationContainer();

        /// <inheritdoc />
        public IContainer ApplicationContainer { get; internal set; }

        /// <summary>
        /// By default, Specify will detect NSubstitute, FakeItEasy and Moq, in that order.
        /// If none are found, or null is returned as the mock factory, then the full TinyIoc 
        /// container will be used without mocking and you will have to configure its dependencies. 
        /// </summary>
        /// <returns>IMockFactory.</returns>
        public IMockFactory MockFactory
        {
            get { return _mockFactory ?? (_mockFactory = new MockDetector().FindAvailableMock()); }
            set { _mockFactory = value; }
        }

        /// <inheritdoc />
        public List<IPerApplicationAction> PerAppDomainActions { get; } = new List<IPerApplicationAction>();

        /// <inheritdoc />
        public List<IPerScenarioAction> PerScenarioActions { get; } = new List<IPerScenarioAction>();

        /// <inheritdoc />
        public bool LoggingEnabled { get; set; } = false;

        /// <inheritdoc />
        public HtmlReportConfiguration HtmlReport { get; } = new HtmlReportConfiguration();

        /// <inheritdoc />
        public void InitializeSpecify()
        {
            ConfigureBddfy();
            ApplicationContainer = BuildApplicationContainer();
            LogSpecifyConfiguration();
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
                this.Log().Debug("Classic HTML report selected.");
            }
            else
            {
                Configurator.BatchProcessors.Add(new HtmlReporter(HtmlReport, new MetroReportBuilder()));
                this.Log().Debug("Metro HTML report selected.");
            }
        }

        private void ConfigureBddfy()
        {
            Configurator.Scanners.StoryMetadataScanner = () => new SpecifyStoryMetadataScanner();
            this.Log().Debug("Added SpecifyStoryMetadataScanner to BDDfy pipeline.");

            if (LoggingEnabled)
            {
                Configurator.Processors.Add(() => new ScenarioLoggingProcessor());
            }

            ConfigureReport();
        }

        private void LogSpecifyConfiguration()
        {
            string containerName;
            using (IContainer container = ApplicationContainer.Get<IContainer>())
            {
                containerName = container.GetType().FullName;
            }

            this.Log().DebugFormat("Bootstrapper: {Bootstrapper}", GetType().FullName);
            this.Log().DebugFormat("ApplicationContainer: {ApplicationContainer}", ApplicationContainer.GetType().FullName);
            this.Log().DebugFormat("ScenarioContainer: {ScenarioContainer}", containerName);
            this.Log().DebugFormat("PerAppDomainActions: {PerAppDomainActionCount}", PerAppDomainActions.Count);

            foreach (var action in PerAppDomainActions)
            {
                this.Log().DebugFormat("- Action: {PerAppDomainAction}", action.GetType().Name);
            }

            this.Log().DebugFormat("PerScenarioActions: {PerScenarioActionCount}", PerScenarioActions.Count);
            foreach (var action in PerScenarioActions)
            {
                this.Log().DebugFormat("- Action: {PerScenarioAction}", action.GetType().Name);
            }

            this.Log().DebugFormat("Logging Enabled = {LoggingEnabled}.", LoggingEnabled);
        }
    }
}