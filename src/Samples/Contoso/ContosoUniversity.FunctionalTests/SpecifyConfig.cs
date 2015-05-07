using Specify;
using Specify.Autofac;
using Specify.Configuration;

namespace ContosoUniversity.FunctionalTests
{
    public class SpecifyConfig : SpecifyBootstrapper
    {
        public override IApplicationContainer CreateApplicationContainer()
        {
            return new AutofacApplicationContainer();
        }
    }
}
