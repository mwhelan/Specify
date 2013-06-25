using TestStack.BDDfy.Processors.HtmlReporter;

namespace Specify
{
    public class SelenoDesignSpecsHtmlReportConfig : DefaultHtmlReportConfiguration
    {
        public override string OutputFileName
        {
            get
            {
                return "ScenarioGeneratorSpecifications.html";
            }
        }

        public override string ReportHeader
        {
            get
            {
                return "Scenario Generator ~ a way to create BDDfy scenarios";
            }
        }

        public override string ReportDescription
        {
            get
            {
                return "Design Specifications";
            }
        }
    }
}