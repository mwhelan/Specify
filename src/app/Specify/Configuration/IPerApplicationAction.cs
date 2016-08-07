namespace Specify.Configuration
{
    /// <summary>
    /// Represents an action to be performed once per Appliction (before and after all scenarios are run).
    /// </summary>
    public interface IPerApplicationAction
    {
        /// <summary>
        /// Action to be performed before any scenarios have run.
        /// </summary>
        void Before();

        /// <summary>
        /// Action to be performed after all scenarios have run.
        /// </summary>
        void After();
    }
}