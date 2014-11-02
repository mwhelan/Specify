using Chill;
using ContosoUniversity.FunctionalTests;
using Specify;
using Specify.Chill.Autofac;

[assembly: ChillTestInitializer(typeof(FunctionalTestInitializer))]

namespace ContosoUniversity.FunctionalTests
{
    public class FunctionalTestInitializer : SpecifyChillTestInitializer<AutofacChillContainer>
    {
    }
}
