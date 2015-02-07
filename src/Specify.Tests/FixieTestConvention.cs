using Fixie;
using Specify.Tests.Tests;

namespace Specify.Tests
{
    public class FixieTestConvention : Convention
    {
        public FixieTestConvention()
        {
            Classes
                .InTheSameNamespaceAs(typeof(SpecifyExtensionTests));
        }
    }
}
