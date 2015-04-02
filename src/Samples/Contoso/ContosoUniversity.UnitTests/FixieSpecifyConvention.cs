using Fixie;
using Specify;

namespace ContosoUniversity.UnitTests
{
    public class FixieSpecifyConvention : Convention
    {
        public FixieSpecifyConvention()
        {
            Classes
                .Where(type => type.IsUnitScenario() || type.IsStoryScenario());

            Methods
                .Where(method => method.Name == "Specify");
        }
    }
}
