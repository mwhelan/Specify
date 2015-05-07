using Specify;
using Specify.Autofac;
using Specify.Configuration;

namespace ContosoUniversity.AcceptanceTests
{
    public class SpecifyConfig : SpecifyBootstrapper
    {
        public override IApplicationContainer CreateApplicationContainer()
        {
            return new AutofacApplicationContainer();
        }
    }
}
