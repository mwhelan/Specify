using FluentAssertions;
using Specify.Configuration;

namespace Specify.Tests.Configuration
{
    public class SpecifyConfigurationSpecs : WithNunit.SpecificationFor<SpecifyConfiguration>
    {
        protected override void EstablishContext()
        {
            
        }

        public void when_applying_a_Specify_configuration()
        {
            SUT = new CustomConfiguration();
        }

        public void Then_should_be_able_to_customise_the_report()
        {
            SUT.HtmlReport.Description.Should().Be("Description1");
            SUT.HtmlReport.Header.Should().Be("Header1");
            SUT.HtmlReport.Name.Should().Be("Name1.html");
            SUT.HtmlReport.Type.Should().Be(HtmlReportConfiguration.ReportType.Metro);
        }

        private class CustomConfiguration : SpecifyConfiguration
        {
            public CustomConfiguration()
            {
                HtmlReport = new HtmlReportConfiguration()
                {
                    Description = "Description1",
                    Header = "Header1",
                    Name = "Name1.html",
                    Type = HtmlReportConfiguration.ReportType.Metro
                };
            }
        }
    }
}
