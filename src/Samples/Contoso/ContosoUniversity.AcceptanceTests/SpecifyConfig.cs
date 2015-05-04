using Specify;
using Specify.Autofac;
using Specify.Configuration;

namespace ContosoUniversity.AcceptanceTests
{
    public class SpecifyConfig : SpecifyConfiguration
    {
        public override IDependencyResolver GetDependencyResolver()
        {
            return new AutofacDependencyResolver();
        }
    }
}
