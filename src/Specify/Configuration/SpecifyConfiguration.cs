using System.Collections.Generic;
using Specify.Containers;

namespace Specify.Configuration
{
    public class SpecifyConfiguration
    {
        public List<ITestRunnerAction> PerAppDomainActions { get; private set; }
        public List<ITestRunnerAction> PerTestActions { get; private set; }

        public bool LoggingEnabled { get; set; }

        public SpecifyConfiguration()
        {
            PerAppDomainActions = new List<ITestRunnerAction>();
            PerTestActions = new List<ITestRunnerAction>();
            LoggingEnabled = false;
        }

        public virtual IContainer GetSpecifyContainer()
        {
            return new IocContainer();
        }
    }
}
