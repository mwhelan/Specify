using TestStack.BDDfy.Reporters.Html;

namespace Specify.Configuration
{
    /// <summary>
    /// Configuration for the BDDfy HTML report.
    /// </summary>
    public class HtmlReportConfiguration : DefaultHtmlReportConfiguration
    {
        /// <summary>
        /// Gets or sets the type of the report.
        /// </summary>
        /// <value>The type of the report.</value>
        public HtmlReportType ReportType { get; set; } = HtmlReportType.Classic;

        /// <summary>
        /// Enum HtmlReportType
        /// </summary>
        public enum HtmlReportType
        {
            /// <summary>
            /// The classic BDDfy HTML report
            /// </summary>
            Classic,
            
            /// <summary>
            /// The newer metro BDDfy HTML report
            /// </summary>
            Metro
        }
    }
}
