using TestStack.BDDfy.Reporters.Html;

namespace ContosoUniversity.UnitTests
{
    public class ReportConfig : DefaultHtmlReportConfiguration
    {
        public override string ReportHeader => "Contoso University";

        public override string ReportDescription => "Unit Specifications";
    }
}