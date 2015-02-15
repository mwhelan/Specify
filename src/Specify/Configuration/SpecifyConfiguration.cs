using System;
using System.Collections.Generic;
using Specify.Containers;

namespace Specify.Configuration
{
    public class SpecifyConfiguration
    {
        public List<ITestRunnerAction> PerAppDomainActions { get; private set; }
        public List<ITestRunnerAction> PerTestActions { get; private set; }

        public SpecifyConfiguration()
        {
            PerAppDomainActions = new List<ITestRunnerAction>();
            PerTestActions = new List<ITestRunnerAction>();
        }

        public virtual IContainer GetSpecifyContainer()
        {
            throw new Exception("You must provide a Specify container");
        }
    }
}
