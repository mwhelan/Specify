using Specify;
using Specify.Autofac;
using Specify.Configuration;

namespace ContosoUniversity.SubcutaneousTests
{
    public class SubcutaneousConfig : SpecifyConfiguration
    {
        public override IDependencyResolver GetDependencyResolver()
        {
            return new AutofacDependencyResolver();
        }
    }
}
