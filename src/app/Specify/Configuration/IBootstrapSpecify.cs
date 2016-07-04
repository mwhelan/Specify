using System.Collections.Generic;

namespace Specify.Configuration
{
    /// <summary>
    /// Interface IBootstrapSpecify
    /// </summary>
    public interface IBootstrapSpecify
    {
        /// <summary>
        /// Gets the container that will be used for the lifetime of the application.
        /// </summary>
        /// <value>The application container.</value>
        IContainer ApplicationContainer { get; }

        /// <summary>
        /// Gets the actions that will be run before and after all the scenarios (at the beginning/end of AppDomain).
        /// </summary>
        /// <value>The per AppDomain actions.</value>
        List<IPerAppDomainActions> PerAppDomainActions { get; }

        /// <summary>
        /// Gets the actions that will be run before and after each scenario.
        /// </summary>
        /// <value>The per scenario actions.</value>
        List<IPerScenarioActions> PerScenarioActions { get; }

        /// <summary>
        /// Gets or sets a value indicating whether logging is enabled.
        /// </summary>
        /// <value><c>true</c> if logging is enabled otherwise <c>false</c>.</value>
        bool LoggingEnabled { get; set; }

        /// <summary>
        /// The configuration values for the BDDfy HTML report.
        /// </summary>
        /// <value>The report header.</value>
        HtmlReportConfiguration HtmlReport { get; }

        /// <summary>
        /// Initializes Specify before any scenarios are run.
        /// </summary>
        void InitializeSpecify();
    }
}