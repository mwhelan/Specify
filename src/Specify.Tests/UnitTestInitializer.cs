using Chill;
using Specify.Chill.Autofac;
using Specify.Tests;

[assembly: ChillTestInitializer(typeof(UnitTestInitializer))]

namespace Specify.Tests
{
    public class UnitTestInitializer : SpecifyChillTestInitializer<AutofacNSubstituteChillContainer>
    {
    }
}
