using TestStack.BDDfy;

namespace Specify.Configuration
{
    internal class BddfyTestEngine : ITestEngine
    {
        public Story Execute(IScenario scenario)
        {
            if (scenario.Examples == null)
            {
                return scenario.BDDfy();
            }
            
            return scenario
                    .WithExamples(scenario.Examples)
                    .BDDfy();
        }
    }
}