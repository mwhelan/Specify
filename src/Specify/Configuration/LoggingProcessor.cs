using Specify.Logging;
using TestStack.BDDfy;
using TestStack.BDDfy.Configuration;

namespace Specify.Configuration
{
    public class LoggingProcessor : IProcessor
    {
        public void Process(Story story)
        {
            var logger = LogProvider.GetLogger("Specify");
            var scenarios = story.Scenarios;
            foreach (var scenario in scenarios)
            {
                var scenarioResult = Configurator.Scanners.Humanize(scenario.Result.ToString());
                logger.DebugFormat("Scenario: {0} Result: {1} Duration: {2} milliseconds.",
                    scenario.Title, scenarioResult, scenario.Duration.Milliseconds);

                foreach (var step in scenario.Steps)
                {
                    var stepResult = Configurator.Scanners.Humanize(step.Result.ToString());
                    logger.DebugFormat("Step: {0} Result: {1} Duration: {2} milliseconds.",
                        step.Title, stepResult, step.Duration.Milliseconds);

                    if (step.Result == Result.Failed)
                    {
                        logger.ErrorException(step.Exception.Message, step.Exception);
                    }
                }
            }
        }

        public ProcessType ProcessType
        {
            get
            {
                return ProcessType.Report;
            }
        }
    }
}
