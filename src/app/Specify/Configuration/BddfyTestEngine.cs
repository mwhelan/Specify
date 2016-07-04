using TestStack.BDDfy;

namespace Specify.Configuration
{
    internal class BddfyTestEngine : ITestEngine
    {
        public Story Execute(IScenario scenario)
        {
            if (scenario.Examples == null)
            {
                return scenario.BDDfy(scenario.Title);
            }

            return scenario
                    .WithExamples(scenario.Examples)
                    .BDDfy(scenario.Title);
        }
    }
}