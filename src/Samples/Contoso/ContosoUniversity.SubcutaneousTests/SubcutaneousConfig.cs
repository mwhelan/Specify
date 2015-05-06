using Specify;
using Specify.Autofac;
using Specify.Configuration;

namespace ContosoUniversity.SubcutaneousTests
{
    public class SubcutaneousConfig : SpecifyConfiguration
    {
        public override IApplicationContainer GetDependencyResolver()
        {
            return new AutofacApplicationContainer();
        }
    }
}
