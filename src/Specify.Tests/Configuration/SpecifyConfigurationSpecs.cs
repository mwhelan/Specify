using FluentAssertions;
using NUnit.Framework;
using Specify.Configuration;

namespace Specify.Tests.Configuration
{
    public class SpecifyConfigurationSpecs
    {
        [Test]
        public void should_be_able_to_provide_custom_conventions()
        {
            var sut = new CustomConfiguration();
            sut.HtmlReport.Description.Should().Be("Description1");
            sut.HtmlReport.Header.Should().Be("Header1");
            sut.HtmlReport.Name.Should().Be("Name1.html");
            sut.HtmlReport.Type.Should().Be(HtmlReportConfiguration.ReportType.Metro);
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
