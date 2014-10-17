using Specify.Configuration;

namespace ContosoUniversity.UnitTests
{
    public class SpecifyConfig : SpecifyConfiguration
    {
        public SpecifyConfig()
        {
            HtmlReport = new HtmlReportConfiguration
            {
                Name = "ContosoUnitTests.html",
                Header = "Contoso University",
                Description = "Unit Tests"
            };

        }
    }
}
