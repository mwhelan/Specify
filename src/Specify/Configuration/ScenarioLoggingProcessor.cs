using Specify.Logging;
using TestStack.BDDfy;
using TestStack.BDDfy.Configuration;

namespace Specify.Configuration
{
    /// <summary>
    /// A BDDfy processor that logs scenario results to the configured log provider.
    /// </summary>
    public class ScenarioLoggingProcessor : IProcessor
    {
        /// <summary>
        /// Logs scenario results for the current story.
        /// </summary>
        /// <param name="story">The story.</param>
        public void Process(Story story)
        {
            var logger = LogProvider.GetLogger("Specify");
            var scenarios = story.Scenarios;

            foreach (var scenario in scenarios)
            {
                var scenarioResult = Configurator.Scanners.Humanize(scenario.Result.ToString());
                logger.InfoFormat("Scenario: {0} Result: {1} Duration: {2} milliseconds.",
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

        /// <summary>
        /// Specifies the BDDfy processor type as Report.
        /// </summary>
        /// <value>The type of the process.</value>
        public ProcessType ProcessType => ProcessType.Report;
    }
}
