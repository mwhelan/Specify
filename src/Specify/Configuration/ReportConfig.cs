namespace Specify.Configuration
{
    public class ReportConfig
    {
        public ReportConfig()
        {
            Header = "BDDfy";
            Name = "BDDfy.html";
            Type = ReportType.Html;
        }
        public string Header { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public ReportType Type { get; set; }

        public enum ReportType
        {
            Html,
            Markdown
        }
    }
}