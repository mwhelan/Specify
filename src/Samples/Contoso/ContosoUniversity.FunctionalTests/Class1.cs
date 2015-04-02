using Fixie;
using Specify;

namespace ContosoUniversity.FunctionalTests
{
    public class FixieSpecifyConvention : Convention
    {
        public FixieSpecifyConvention()
        {
            Classes
                .Where(type => type.IsUnitScenario() || type.IsStoryScenario());

            Methods
                .Where(method => method.Name == "ExecuteTest");
        }
    }
}
