using Specify.Configuration;
using Specify.Containers;
using Specify.Examples.Autofac;

namespace ContosoUniversity.UnitTests
{
    public class SpecifyConfig : SpecifyConfiguration
    {
        public override IContainer GetSpecifyContainer()
        {
            return new AutofacNSubstituteContainer();
        }
    }
}
