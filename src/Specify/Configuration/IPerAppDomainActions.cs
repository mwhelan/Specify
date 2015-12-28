namespace Specify.Configuration
{
    /// <summary>
    /// Represents an action to be performed per AppDomain (before and after scenarios are run).
    /// </summary>
    public interface IPerAppDomainActions
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