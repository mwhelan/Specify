namespace Specify.Configuration
{
    /// <summary>
    /// Represents an action to be performed before and after each scenario.
    /// </summary>
    public interface IPerScenarioActions
    {
        /// <summary>
        /// Action to be performed before each scenario.
        /// </summary>
        void Before(IContainer container);

        /// <summary>
        /// Action to be performed after each scenario.
        /// </summary>
        void After();
    }
}
