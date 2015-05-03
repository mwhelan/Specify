using Specify.Configuration;
using TestStack.BDDfy.Configuration;
using TestStack.BDDfy.Reporters.Html;

namespace ContosoUniversity.UnitTests
{
    public class SpecifyConfig : SpecifyConfiguration
    {
        //public override IDependencyResolver GetDependencyResolver()
        //{
        //    return new AutoMockingContainer(new NSubstituteMockFactory());
        //}
        public SpecifyConfig()
        {
            Configurator.BatchProcessors.HtmlReport.Disable();
            Configurator.BatchProcessors.Add(new HtmlReporter(new ReportConfig()));
        }
    }
}
