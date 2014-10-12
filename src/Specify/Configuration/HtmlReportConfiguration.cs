namespace Specify.Configuration
{
    public class HtmlReportConfiguration
    {
        public HtmlReportConfiguration()
        {
            Header = "Specify";
            Name = "SpecifySpecs.html";
            Type = ReportType.Html;
        }
        public string Header { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public ReportType Type { get; set; }

        public enum ReportType
        {
            Html,
            Metro
        }
    }
}