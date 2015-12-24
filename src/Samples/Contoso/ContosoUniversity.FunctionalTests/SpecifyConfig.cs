using Autofac;
using Specify.Autofac;

namespace ContosoUniversity.FunctionalTests
{
    public class SpecifyConfig : SpecifyAutofacBootstrapper
    {
        public override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<FunctionalTestsAutofacModule>();
        }
    }
}
