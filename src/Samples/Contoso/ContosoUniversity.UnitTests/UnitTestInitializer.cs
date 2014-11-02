using Chill;
using ContosoUniversity.UnitTests;
using Specify;
using Specify.Chill.Autofac;

[assembly: ChillTestInitializer(typeof(UnitTestInitializer))]

namespace ContosoUniversity.UnitTests
{
    public class UnitTestInitializer : SpecifyChillTestInitializer<AutofacNSubstituteChillContainer>
    {
    }
}
