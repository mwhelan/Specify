using Specify;
using Specify.Configuration;
using Specify.WithAutofacNSubstitute;

namespace ContosoUniversity.FunctionalTests
{
    public class SpecifyConfig : SpecifyConfiguration
    {
        public override ISpecifyContainer GetSpecifyContainer()
        {
            return new AutofacContainer();
        }
    }
}
