using TestStack.BDDfy.Reporters.Html;

namespace ContosoUniversity.UnitTests
{
    public class ReportConfig : DefaultHtmlReportConfiguration
    {
        public override string ReportHeader
        {
            get { return "Contoso University"; }
        }

        public override string ReportDescription
        {
            get { return "Unit Specifications"; }
        }
    }
}