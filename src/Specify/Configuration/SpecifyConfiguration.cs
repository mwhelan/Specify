using System;
using System.Collections.Generic;

namespace Specify.Configuration
{
    public class SpecifyConfiguration
    {
        public HtmlReportConfiguration HtmlReport { get; protected set; }
        public List<ITestRunnerAction> PerAppDomainActions { get; private set; }
        public List<ITestRunnerAction> PerTestActions { get; private set; }

        public SpecifyConfiguration()
        {
            PerAppDomainActions = new List<ITestRunnerAction>();
            PerTestActions = new List<ITestRunnerAction>();
        }

        public virtual ISpecifyContainer GetSpecifyContainer()
        {
            throw new Exception("You must provide a Specify container");
        }
    }
}
