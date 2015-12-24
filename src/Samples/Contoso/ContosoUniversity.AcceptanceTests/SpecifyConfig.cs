using Autofac;
using Specify;
using Specify.Autofac;
using Specify.Configuration;

namespace ContosoUniversity.AcceptanceTests
{
    public class SpecifyConfig : SpecifyAutofacBootstrapper
    {
        public override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<AcceptanceTestsAutofacModule>();
        }
    }
}
