using System.Collections.Generic;
using NSubstitute;
using Specify.Configuration;

namespace Specify.Tests.Configuration
{
    internal class StubConfig : DefaultBootstrapper
    {
        public StubConfig()
        {
            ApplicationContainer = Substitute.For<IContainer>();
            StubPerScenarioAction.Reset();
            PerAppDomainActions.Add(new StubPerAppDomainAction { Name = "PerAppDomainAction 1" });
            PerAppDomainActions.Add(new StubPerAppDomainAction { Name = "PerAppDomainAction 2" });
            PerScenarioActions.Add(new StubPerScenarioAction { Name = "PerTestAction 1" });
            PerScenarioActions.Add(new StubPerScenarioAction { Name = "PerTestAction 2" });
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

        public void Before()
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

        public void Before(IContainer container)
        {
            BeforeActions.Add(Name);
        }

        public void After()
        {
            AfterActions.Add(Name);
        }
    }
}
