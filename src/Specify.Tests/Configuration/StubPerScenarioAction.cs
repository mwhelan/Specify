using System.Collections.Generic;
using Specify.Configuration;

namespace Specify.Tests.Configuration
{
    internal class StubConfig : SpecifyBootstrapper
    {
        public StubConfig()
        {
            StubPerScenarioAction.Reset();
            PerAppDomainActions.Add(new StubPerAppDomainAction { Name = "PerAppDomainAction 1" });
            PerAppDomainActions.Add(new StubPerAppDomainAction { Name = "PerAppDomainAction 2" });
            PerTestActions.Add(new StubPerScenarioAction { Name = "PerTestAction 1" });
            PerTestActions.Add(new StubPerScenarioAction { Name = "PerTestAction 2" });
        }
    }
    internal class StubPerAppDomainAction : IPerAppDomainActions
    {
        public string Name { get; set; }

        public static List<string> BeforeActions = new List<string>();
        public static List<string> AfterActions = new List<string>();

        public static void Reset()
        {
            BeforeActions.Clear();
            AfterActions.Clear();
        }

        public void Before(IApplicationContainer container)
        {
            BeforeActions.Add(Name);
        }

        public void After()
        {
            AfterActions.Add(Name);
        }
    }

    internal class StubPerScenarioAction : IPerScenarioActions
    {
        public string Name { get; set; }

        public static List<string> BeforeActions = new List<string>();
        public static List<string> AfterActions = new List<string>();
        
        public static void Reset()
        {
            BeforeActions.Clear();
            AfterActions.Clear();
        }

        public void Before(IScenarioContainer container)
        {
            BeforeActions.Add(Name);
        }

        public void After()
        {
            AfterActions.Add(Name);
        }
    }
}
