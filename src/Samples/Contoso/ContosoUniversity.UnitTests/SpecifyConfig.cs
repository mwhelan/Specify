using Specify.Configuration;
using TestStack.BDDfy.Configuration;
using TestStack.BDDfy.Reporters.Html;

namespace ContosoUniversity.UnitTests
{
    public class SpecifyConfig : SpecifyTinyBootstrapper
    {
        public SpecifyConfig()
        {
            LoggingEnabled = true;
            HtmlReport.ReportHeader = "Contoso University";
            HtmlReport.ReportDescription = "Unit Specifications";
            HtmlReport.ReportType = HtmlReportConfiguration.HtmlReportType.Metro;
            HtmlReport.OutputFileName = "metro.html";
        }
    }
}
