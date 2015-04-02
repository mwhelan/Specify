using TestStack.BDDfy;

namespace Specify.Configuration
{
    internal class BddfyTestEngine : ITestEngine
    {
        public void Execute(IScenario scenario)
        {
            if (scenario.Examples == null)
            {
                scenario
                    .BDDfy();
            }
            else
            {
                scenario
                    .WithExamples(scenario.Examples)
                    .BDDfy();
            }
        }
    }
}