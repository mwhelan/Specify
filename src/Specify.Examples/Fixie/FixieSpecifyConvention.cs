using Fixie;

namespace Specify.Examples.Fixie
{
    public class FixieSpecifyConvention : Convention
    {
        public FixieSpecifyConvention()
        {
            Classes
                .Where(type => type.IsScenario());

            Methods
                .Where(method => method.Name == "Specify");
        }
    }
}