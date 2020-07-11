using System;
using ApiTemplate.Api.Domain.Common;
using Specify;
using Specify.Configuration;

namespace Specs.Library.Data
{
    public class ResetSystemTimeAction : IPerScenarioAction
    {
        public void Before<TSut>(IScenario<TSut> scenario) where TSut : class
        {
        }

        public void After()
        {
            SystemTime.Reset();
        }

        public bool ShouldExecute(Type type)
        {
            return true;
        }

        public int Order { get; set; }
    }
}