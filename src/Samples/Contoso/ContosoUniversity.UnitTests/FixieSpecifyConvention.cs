using Fixie;
using Specify;

namespace ContosoUniversity.UnitTests
{
    public class FixieSpecifyConvention : Convention
    {
        public FixieSpecifyConvention()
        {
            Classes
                .Where(type => type.IsSpecificationFor() || type.IsScenarioFor());

            Methods
                .Where(method => method.Name == "Specify");
        }
    }
}
