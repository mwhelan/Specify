using Specify.Configuration;

namespace ContosoUniversity.AcceptanceTests
{
    public class SpecifyConfig : SpecifyConfiguration
    {
        public SpecifyConfig()
        {
            HtmlReport.Name = "ContosoAcceptanceTests.html";
            HtmlReport.Header = "Contoso University";
            HtmlReport.Description = "Acceptance Tests";
        }
    }
}
