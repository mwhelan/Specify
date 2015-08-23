using System.Collections.Generic;

namespace Specify.Configuration
{
    public class SpecifyBootstrapper
    {
        public List<IPerAppDomainActions> PerAppDomainActions { get; private set; }
        public List<IPerScenarioActions> PerScenarioActions { get; private set; }

        public bool LoggingEnabled { get; set; }

        public SpecifyBootstrapper()
        {
            PerAppDomainActions = new List<IPerAppDomainActions>();
            PerScenarioActions = new List<IPerScenarioActions>();
            LoggingEnabled = false;
        }

        public virtual IApplicationContainer CreateApplicationContainer()
        {
            return new DefaultApplicationContainer();
        }
    }
}
