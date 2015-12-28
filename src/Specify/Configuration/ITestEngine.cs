using TestStack.BDDfy;

namespace Specify.Configuration
{

    /// <summary>
    /// Represents the class that exectues scenarios
    /// </summary>
    internal interface ITestEngine
    {
        /// <summary>
        /// Executes the specified scenario.
        /// </summary>
        /// <param name="scenario">The scenario.</param>
        /// <returns>Story.</returns>
        Story Execute(IScenario scenario);
    }
}