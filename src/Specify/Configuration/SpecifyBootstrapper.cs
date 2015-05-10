using System.Collections.Generic;

namespace Specify.Configuration
{
    public class SpecifyBootstrapper
    {
        public List<IPerAppDomainActions> PerAppDomainActions { get; private set; }
        public List<IPerScenarioActions> PerTestActions { get; private set; }

        public bool LoggingEnabled { get; set; }

        public SpecifyBootstrapper()
        {
            PerAppDomainActions = new List<IPerAppDomainActions>();
            PerTestActions = new List<IPerScenarioActions>();
            LoggingEnabled = false;
        }

        public virtual IApplicationContainer CreateApplicationContainer()
        {
            return new DefaultApplicationContainer();
        }
    }
}
