using Autofac;
using Specify;
using Specify.Autofac;
using Specify.Configuration;

namespace ContosoUniversity.AcceptanceTests
{
    public class SpecifyConfig : DefaultAutofacBootstrapper
    {
        public override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<AcceptanceTestsAutofacModule>();
        }
    }
}
