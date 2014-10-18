using Specify.Configuration;
using Specify.Containers;

namespace ContosoUniversity.AcceptanceTests
{
    public class SpecifyConfig : SpecifyConfiguration
    {
        public SpecifyConfig()
        {
            HtmlReport.Name = "ContosoAcceptanceTests.html";
            HtmlReport.Header = "Contoso University";
            HtmlReport.Description = "Acceptance Tests";
        }

        public override IDependencyResolver DependencyResolver()
        {
            return new AutofacDependencyResolver(null);
        }
    }
}
