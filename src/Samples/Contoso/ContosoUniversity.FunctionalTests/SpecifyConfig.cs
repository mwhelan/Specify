using Autofac;
using Specify.Autofac;

namespace ContosoUniversity.FunctionalTests
{
    public class SpecifyConfig : DefaultAutofacBootstrapper
    {
        public override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<FunctionalTestsAutofacModule>();
        }
    }
}
