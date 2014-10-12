namespace Specify.Configuration
{
    public class SpecifyConventions
    {
        public HtmlReportConfiguration HtmlReport { get; protected set; }

        public SpecifyConventions()
        {
            HtmlReport = new HtmlReportConfiguration();
        }

        public virtual void BeforeAllTests()
        {
        }

        public virtual void AfterAllTests()
        {
        }
    }
}
