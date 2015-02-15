using Specify;
using Specify.Configuration;
using Specify.Containers;
using Specify.Examples.Autofac;

namespace ContosoUniversity.FunctionalTests
{
    public class SpecifyConfig : SpecifyConfiguration
    {
        public override IContainer GetSpecifyContainer()
        {
            return new AutofacContainer();
        }
    }
}
