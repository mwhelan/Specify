using System;
using System.Collections.Generic;

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

        public virtual IDependencyResolver GetDependencyResolver()
        {
            // try to find class that implements IDependencyResolver?
            throw new NotImplementedException();
        }
    }
}
