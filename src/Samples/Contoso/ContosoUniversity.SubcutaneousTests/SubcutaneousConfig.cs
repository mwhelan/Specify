using Specify;
using Specify.Autofac;
using Specify.Configuration;

namespace ContosoUniversity.SubcutaneousTests
{
    public class SubcutaneousConfig : SpecifyBootstrapper
    {
        public override IApplicationContainer CreateApplicationContainer()
        {
            return new AutofacApplicationContainer();
        }
    }
}
