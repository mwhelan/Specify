using Fixie;

namespace Specify.Examples.Fixie
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