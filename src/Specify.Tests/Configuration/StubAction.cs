using System.Collections.Generic;
using Specify.Configuration;

namespace Specify.Tests.Configuration
{
    internal class StubConfig : SpecifyConfiguration
    {
        public StubConfig()
        {
            StubAction.Reset();
            PerAppDomainActions.Add(new StubAction { Name = "PerAppDomainAction 1" });
            PerAppDomainActions.Add(new StubAction { Name = "PerAppDomainAction 2" });
            PerTestActions.Add(new StubAction { Name = "PerTestAction 1" });
            PerTestActions.Add(new StubAction { Name = "PerTestAction 2" });
        }
    }
    internal class StubAction : TestRunnerAction
    {
        public string Name { get; set; }

        public static List<string> BeforeActions = new List<string>();
        public static List<string> AfterActions = new List<string>();
        
        public StubAction()
        {
            BeforeAction = Start;
            AfterAction = Stop;
        }

        private void Start()
        {
            BeforeActions.Add(Name);
        }

        private void Stop()
        {
            AfterActions.Add(Name); 
        }

        public static void Reset()
        {
            BeforeActions.Clear();
            AfterActions.Clear();
        }
    }
}
