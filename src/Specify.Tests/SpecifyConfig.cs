using Specify.Configuration;
using Specify.Providers;

namespace Specify.Tests
{
    public class SpecifyConfig : SpecifyConfiguration
    {
        public SpecifyConfig()
        {
            HtmlReport = new HtmlReportConfiguration
            {
                Name = "SpecifySpecs.html",
                Header = "Specify",
                Description = "Specifications"
            };
        }

        public override ISpecifyContainer GetSpecifyContainer()
        {
            return new AutofacNSubstituteContainer();
        }
    }
}
