using System.Collections.Generic;
using System.Linq;
using Specify.Configuration;

namespace Specify.IntegrationTests.Containers
{
    public class ScenarioScope
    {
        private readonly IContainer _applicationContainer;
        private readonly IList<IPerScenarioAction> _actions;

        public ScenarioScope(IContainer applicationContainer, IEnumerable<IPerScenarioAction> actions)
        {
            _applicationContainer = applicationContainer;
            _actions = actions.OrderBy(x => x.Order).ToList();
        }

        public ContainerFor<TSut> BeginScope<TSut>(IScenario<TSut> scenario) 
            where TSut : class
        {
            var container = _applicationContainer.Get<IContainer>();
            return new ContainerFor<TSut>(container);
        }

        public void EndScope<TSut>(IScenario<TSut> scenario)
            where TSut : class
        {
            scenario.Container?.Dispose();
        }
    }
}
