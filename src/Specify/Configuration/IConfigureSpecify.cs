using System.Collections.Generic;

namespace Specify.Configuration
{
    public interface IConfigureSpecify
    {
        IContainer ApplicationContainer { get; }
        List<IPerAppDomainActions> PerAppDomainActions { get; }
        List<IPerScenarioActions> PerScenarioActions { get; }
        bool LoggingEnabled { get; set; }
    }
}