using Fixie;

namespace Specify.Tests
{
    public class FixieSpecifyConvention : Convention
    {
        public FixieSpecifyConvention()
        {
            Classes
                .Where(type => type.IsSpecificationFor() || type.IsScenarioFor());

            Methods
                .Where(method => method.Name == "ExecuteTest");
        }
    }
}