using System.Collections.Generic;
using System.Linq;

namespace Specify.Configuration.Examples
{
    public class ExampleScope : IExampleScope
    {
        private readonly IContainer _rootContainer;
        private IEnumerable<IPerScenarioAction> _actions;

        public ExampleScope(IContainer rootContainer)
        {
            _rootContainer = rootContainer;
        }

        public void BeginScope<T>(IScenario<T> scenario)
            where T : class
        {
            var childContainer = _rootContainer.Get<IContainer>();
            scenario.Container = new ContainerFor<T>(childContainer);

            _actions = childContainer.Get<IEnumerable<IPerScenarioAction>>();
            foreach (var action in _actions)
            {
                if (action.ShouldExecute(scenario.GetType()))
                {
                    action.Before(scenario);
                }
            }
        }

        public void EndScope<T>(IScenario<T> scenario)
            where T : class
        {
            foreach (var action in Enumerable.Reverse<IPerScenarioAction>(_actions))
            {
                if (action.ShouldExecute(scenario.GetType()))
                {
                    action.After();
                }
            }

            scenario.Container?.Dispose();
        }
    }
}
