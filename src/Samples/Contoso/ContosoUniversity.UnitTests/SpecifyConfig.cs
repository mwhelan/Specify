using Specify;
using Specify.Configuration;
using Specify.WithAutofacNSubstitute;

namespace ContosoUniversity.UnitTests
{
    public class SpecifyConfig : SpecifyConfiguration
    {
        public override ISpecifyContainer GetSpecifyContainer()
        {
            return new AutofacNSubstituteContainer();
        }
    }
}
