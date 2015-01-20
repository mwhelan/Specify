using FluentAssertions;
using Specify.Configuration;

namespace Specify.Tests.Configuration
{
    public class ApplyingSpecifyConfiguration : WithNunit.SpecificationFor<ApplyingSpecifyConfiguration.CustomConfiguration>
    {
        //protected override void CreateSut()
        //{
        //    SUT = new CustomConfiguration();
        //}

        //public void When_applying_a_Specify_configuration()
        //{
        //}

        public void Then_should_be_able_to_customise_the_report_header()
        {
            SUT.HtmlReport.Header.Should().Be("Header1");
        }

        public void AndThen_the_report_description()
        {
            SUT.HtmlReport.Description.Should().Be("Description1");
        }

        public void AndThen_the_report_file_name()
        {
            SUT.HtmlReport.Name.Should().Be("Name1.html");
        }

        public void AndThen_the_report_type()
        {
            SUT.HtmlReport.Type.Should().Be(HtmlReportConfiguration.ReportType.Metro);
        }

        public override string Title
        {
            get
            {
                return "Specify Configuration";
            }
        }

        public class CustomConfiguration : SpecifyConfiguration
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
