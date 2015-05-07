using System.Collections.Generic;

namespace Specify.Configuration
{
    public class SpecifyBootstrapper
    {
        public List<ITestRunnerAction> PerAppDomainActions { get; private set; }
        public List<ITestRunnerAction> PerTestActions { get; private set; }

        public bool LoggingEnabled { get; set; }

        public SpecifyBootstrapper()
        {
            PerAppDomainActions = new List<ITestRunnerAction>();
            PerTestActions = new List<ITestRunnerAction>();
            LoggingEnabled = false;
        }

        public virtual IApplicationContainer CreateApplicationContainer()
        {
            // try to find class that implements IApplicationContainer?
            return new DefaultApplicationContainer();
        }
    }
}
