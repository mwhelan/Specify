using System;
using Specify;
using Specify.Configuration;

namespace Specs.Library.Data
{
    public class ResetDatabaseAction : IPerScenarioAction
    {
        public void Before<TSut>(IScenario<TSut> scenario) where TSut : class
        {
            scenario.Container.Get<IDbFactory>().ResetData();
        }

        public void After()
        {
        }

        public bool ShouldExecute(Type type)
        {
            return true;
        }

        public int Order { get; set; }
    }
}