using Specify.Configuration;
using TestStack.BDDfy.Configuration;
using TestStack.BDDfy.Reporters.Html;

namespace ContosoUniversity.UnitTests
{
    public class SpecifyConfig : SpecifyBootstrapper
    {
        public SpecifyConfig()
        {
            Configurator.BatchProcessors.HtmlReport.Disable();
            Configurator.BatchProcessors.Add(new HtmlReporter(new ReportConfig()));
        }
    }
}
