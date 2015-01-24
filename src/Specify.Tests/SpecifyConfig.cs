using Specify.Configuration;
using Specify.WithAutofacNSubstitute;

namespace Specify.Tests
{

    public class SpecifyConfig : SpecifyConfiguration
    {
        public override ISpecifyContainer GetSpecifyContainer()
        {
            return new AutofacNSubstituteContainer();
        }
    }
}
