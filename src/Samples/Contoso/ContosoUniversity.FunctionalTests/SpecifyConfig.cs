using Specify.Configuration;

namespace ContosoUniversity.FunctionalTests
{
    public class SpecifyConfig : SpecifyConfiguration
    {
        public SpecifyConfig()
        {
            HtmlReport = new HtmlReportConfiguration();
            HtmlReport.Name = "ContosoAcceptanceTests.html";
            HtmlReport.Header = "Contoso University";
            HtmlReport.Description = "Acceptance Tests";
        }
    }
}
